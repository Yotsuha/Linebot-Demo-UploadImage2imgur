using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class LineBotWebHookController : isRock.LineBot.LineWebHookControllerBase
    {
        [Route("api/LineWebHookSample")]
        [HttpPost]
        public IHttpActionResult POST()
        {
            isRock.LineBot.Event LineEvent = null;
            //取得Web.config中的 app settings
            var token = System.Configuration.ConfigurationManager.AppSettings["ChannelAccessToken"];
            var CLIENT_ID = System.Configuration.ConfigurationManager.AppSettings["Imgur_CLIENT_ID"];
            var CLIENT_SECRET = System.Configuration.ConfigurationManager.AppSettings["Imgur_CLIENT_SECRET"];

            try
            {
                //抓取Line Event
                LineEvent = this.ReceivedMessage.events.FirstOrDefault();
                //設定抓取Web.Config 
                //this.ChannelAccessToken = channelAccessToken;
                //取得Line Event(範例，只取第一個)
                //配合Line verify 
                if (LineEvent.replyToken == "00000000000000000000000000000000") return Ok();
                //回覆訊息
                if (LineEvent.type == "message")
                {
                    if (LineEvent.message.type == "text") //收到文字
                        this.ReplyMessage(LineEvent.replyToken, "你說了:" + LineEvent.message.text);
                    if (LineEvent.message.type == "sticker") //收到貼圖
                        this.ReplyMessage(LineEvent.replyToken, 1, 2);

                    //收到用戶傳來的圖片(重點在這一段)
                    if (LineEvent.message.type == "image") 
                    {
                        //建立 ImgurClient
                        var client = new ImgurClient(CLIENT_ID, CLIENT_SECRET);
                        var endpoint = new ImageEndpoint(client);
                        IImage image;
                        //從LineEvent取得用戶上傳的圖檔bytes
                        var byteArray=isRock.LineBot.Utility.GetUserUploadedContent(LineEvent.message.id, token);
                        //取得圖片檔案FileStream
                        Stream stream = new MemoryStream(byteArray);
                        using (stream)
                        {
                            image = endpoint.UploadImageStreamAsync(stream).GetAwaiter().GetResult();
                        }

                        //上傳成功之後，image.Link會回傳 url
                        //建立文字訊息
                        isRock.LineBot.TextMessage TextMsg = new isRock.LineBot.TextMessage(image.Link);
                        //建立圖形訊息(用上傳後的網址)
                        isRock.LineBot.ImageMessage imageMsg = new isRock.LineBot.ImageMessage(new Uri(image.Link), new Uri(image.Link));
                        //建立集合
                        var Messages = new List<isRock.LineBot.MessageBase>();
                        Messages.Add(TextMsg);
                        Messages.Add(imageMsg);

                        //一次把集合中的多則訊息回覆給用戶
                        this.ReplyMessage(LineEvent.replyToken, Messages);
                    }
                }
                //response OK
                return Ok();
            }
            catch (Exception ex)
            {
                //如果發生錯誤，傳訊息給Admin
                this.ReplyMessage(LineEvent.replyToken, "發生錯誤:\n" + ex.Message);
                //response OK
                return Ok();
            }
        }
    }
}
