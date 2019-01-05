
# Linebot-Demo-UploadImage2imgur

## 這個範例是展示透過 Line bot 拍照或上傳圖檔後，將其上傳到 Imgur 作為圖床
    
     
     
LINE Bot畫面
===
當用戶上傳圖檔或拍照給這個 LINE Bot之後，Bot會將取得的檔案(Bytes)上傳到 Imgur 並且取得 url，顯示在畫面上。

 ![](https://i.imgur.com/rTLys7o.png)


如何佈署專案
===
* 請 clone 之後，修改 web.config 中的 ChannelAccessToken, Imgur_CLIENT_ID, 與 Imgur_CLIENT_SECRET
```xml
  <appSettings>
    <add key="ChannelAccessToken" value="~~~ 請填入你的 ChannelAccessToken ~~~"/> 
    <add key="Imgur_CLIENT_ID" value="~~~ 請填入你的 Imgur_CLIENT_ID ~~~"/>
    <add key="Imgur_CLIENT_SECRET" value="~~~ 請填入你的 Imgur_CLIENT_SECRET ~~~"/>
  </appSettings>
```
* 建議使用Ngrok進行測試 <br/>
(可參考 https://youtu.be/kCga1_E-ijs ) 
* LINE Bot後台的WebHook設定，其位置為 Http://你的domain/api/LineAccountBook

線上課程 與 電子書 
===
LineBotSDK線上教學課程: <br/>
https://www.udemy.com/line-bot <br/>
 <br/>
電子書購買位置(包含範例完整說明): <br/>
https://www.pubu.com.tw/ebook/103305 <br/>