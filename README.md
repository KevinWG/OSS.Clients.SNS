# 快速了解
当前项目主要是针对社交网站的接口封装，在项目前期主要以微信平台sdk开发为主，根据接口功能层次简单分类，  
此项目以标准库的形式提供服务，也就是可以同时支持.Net Framework(4.6及以上版本) 和 .Net Core  
如果有问题，也可以在公众号(osscore)中提问:   
![osscore](http://img1.static.osscore.com/wei_qr.jpg)  
	
一. **授权对接模块**  (Oauth)

    nuget下安装命令：**Install-Package OSS.Clients.Oauth.WX**   
   	
用户授权（oauth2.0），用户授权基础信息
    
二. **会话消息模块**   (msg)

    nuget下安装命令：**Install-Package OSS.Clients.Msg.Wechat**

会话管理，接收用户的会话信息，以及对应的响应

三. **公众号高级功能**  (Platform)

 1.***AccessToken***

    nuget下安装命令：**Install-Package OSS.Clients.Platform.WX.AccessToken**

```
    这里主要是获取AccessToken接口的封装

    WXPlatTokenApi - 公众号的正常Token接口
    WXAgentPlatTokenApi - 代理平台的Token的接口
```
2.***基础信息部分***

    nuget下安装命令：**Install-Package OSS.Clients.Platform.WX.Basic**

```
    这里主要是基础信息相关接口

    WXPlatUserApi - 微信公众号用户信息相关接口
    WXPlatIpApi - 微信服务器信息接口
    WXPlatKfApi - 微信客服接口
    WXPlatMassApi - 微信群发消息相关接口
    WXPlatMediaApi - 微信素材相关接口
    WXPlatMenuApi - 微信菜单相关接口
    WXPlatQrApi - 微信二维码相关接口

```

3.***其他接口***

    nuget下安装命令：**Install-Package OSS.Clients.Platform.WX**

```
    这里主要是公众号其他相关接口

    WXPlatAssistApi - 微信jssdk辅助接口
    WXPlatCardApi - 微信卡信息相关接口
    WXPlatShakeApi - 微信摇一摇相关接口
    WXPlatAppApi - 微信小程序相关接口
    WXPlatStatApi - 微信统计相关接口
    WXPlatStoreApi - 微信门店管理相关接口

```

4.***代理平台接口***

    nuget下安装命令：**Install-Package OSS.Clients.Platform.WX.Agent**

```
    这里主要是公众号代理服务平台的相关接口
```



#  使用

### 一. 调用示例

1.基础授权调用（sns文件夹下）  
```csharp
	//声明配置信息
 private static AppConfig m_Config = new AppConfig()
  {
      AppSource = "11",
      AppId = "你的appId",
      AppSecret = "你的secretkey"
  };
  // 接口api实例
  private static WXOauthApi m_AuthApi = new WXOauthApi(m_Config);
  
  // 获取微信授权地址
  public ActionResult auth()
  {
      var res = m_AuthApi.GetAuthorizeUrl("http://www.social.com/wxoauth/callback",
				 AuthClientType.WXPlat);
      return Redirect(res);
  }
  //  微信回调页，此页面获取accesstoken 获取用户基础信息
  public ActionResult callback(string code, string state)
  {
      var tokecRes = m_AuthApi.GetAuthAccessToken(code);
      if (tokecRes.IsSuccess())
      {
          var userInfoRes = m_AuthApi.GetWXAuthUserInfo(tokecRes.AccessToken, tokecRes.OpenId);
          return Content("你已成功获取用户信息!");
      }
      return Content("获取用户授权信息失败!");
  }
```  
2.会话调用 （Msg文件夹下）  
a.首先声明配置信息
```csharp
// 声明配置
private static readonly WXChatServerConfig config = new WXChatServerConfig()
  {
      Token = "你的token",
      EncodingAesKey = "你的加密key",
      SecurityType = WXSecurityType.Safe,//  在微信段设置的安全模式
      AppId = "你的appid"   //  
  };
```  
b. 定义一个处理句柄（可以实现一个自己的Handler，继承自WXChatHandler 即可）
```csharp
     private static readonly WXChatHandler msgService = new WXChatHandler(config);
```  
c. 调用时将当前请求的内容传入程序入口即可：  
```csharp
   using (StreamReader reader = new StreamReader(Request.InputStream))
   {
       requestXml = reader.ReadToEnd();
   }
   try
   {
       var res = msgService.Process( requestXml, signature, timestamp, nonce,echostr);
       if (res.IsSuccess())        
           return Content(res.data);
   }
   catch (Exception ex)
   {
   }            
   return Content("success");
```  
其中WXChatHandler 可以是自己继承WXChatHandler 实现的具体处理类，通过重写相关用户事件返回对应结果即可。  

3.高级功能调用（Platform文件夹下）  
微信公众号的其他高级功能接口都需要一个全局的accesstoken接口，像推送模块信息等，accesstoken自动获取已经被封装在sdk底层的请求处理中，默认会使用系统缓存保存，过期自动更新，如果需要保存到像redis中可以通过oscommon中的缓存模块注入，添加一个针对sns的缓存模块实现就可以了（后续给出一个示例），access和appid一一对应，不用担心多个公众号的冲突问题。  

a.  声明配置信息：
```csharp
//声明配置信息
private static AppConfig m_Config = new AppConfig()
{
      AppSource = "11",
      AppId = "你的appId",
      AppSecret = "你的secretkey"
};
```  
b. 声明一个实例：  
```csharp
    private static readonly WXPlatMassApi m_OffcialApi = new WXPlatMassApi(m_Config);
```  
c.  具体使用
```csharp
  m_OffcialApi.SendTemplateAsync("openid","templateId","url",new {})
```
当前这部分接口框架逻辑部分已经处理完毕，公号主要功能已经完成，还差小店和设备管理等部分的接口完善，后续将很快更新，如果有需要的也可以自己实现或贡献过来，添加一个新接口只需要几行代码即可，详见贡献代码。

针对业务的实际使用场景，微信公众号接口做了简单拆解，AccessToken等为独立SDK。
所有独立的SDK引用OSS.Clients.Platform.WX.Base类库，在此类库下提供WXPlatConfigProvider统一配置，主要是方便用户使用其他部分SDK时实现AccessToken的统一获取，如：
使用OSS.Clients.Platform.WX.AccessToken实现的AccessToken统一缓存管理项目，在使用OSS.Clients.Platform.WX.Basic的项目中实现WXPlatConfigProvider下的配置AccessToken的获取方法

###  二.  贡献代码
这个项目当前主要集中在微信sdk处理，微信部分主体框架部分已经完成，需要对接口进行补充，根据已经封装完毕的框架完成一个接口将非常简单,以获取用户基本信息为例，简单分为以下两部步：

1.声明对象实体
```csharp
 // 获取用户基本信息请求实体
 public class WXPlatUserInfoReq
 {
     public string openid { get; set; }
     public string lang { get; set; }
 }
 // 响应实体，继承WXBaseResp
 public class WXPlatUserInfoResp:WXBaseResp
 {
     public string openid { get; set; }
     public string nickname { get; set; }
     //...  字段省略
     public List<int> tagid_list { get; set; }
 }
```
   我写了个js页面，快速通过微信文档字段描述生成实体
   [Gist地址](https://gist.github.com/KevinWG/8db0f960d1efe97d1b1034ef1a7cbc24)，[开源中国地址](http://git.oschina.net/KevinW/codes/0tj5pcnuhsab8yvk3wrlq98)

2.功能实现
```csharp
public WXPlatUserInfoResp GetUserInfo(WXPlatUserInfoReq userReq)
{
    var req = new OssHttpRequest();
    req.HttpMethod = HttpMethod.Get;
    req.AddressUrl = string.Concat(m_ApiUrl,
         $"/cgi-bin/user/info?openid={userReq.openid}&lang={userReq.lang}");
   //  请求地址中的AccessToken 底层会自动补充
    return RestCommonOffcial<WXPlatUserInfoResp>(req);
}
```
3.添加单元测试（非必须）
```csharp
[TestMethod]
public void GetUserInfoTest()
{
    var res = m_Api.GetUserInfo(new WXPlatUserInfoReq() 
			{openid = "o7gE1s6mygEKgopVWp7BBtEAqT-w" });
    Assert.IsTrue(res.IsSuccess());
}
```
剩下的通过git提交即可。

### 三. 实现模式介绍
     尽快完善
     
### 四. 项目依赖
当前项目依赖于我以前写的开源项目 [OSS.Common](https://github.com/KevinWG/OSS.Common)(全局错误实体，和模块注入)  和  [OSS.Http](https://github.com/KevinWG/OSS.Http)（http请求）   都比较简单小巧,只有几个class，不用担心臃肿
