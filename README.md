# 简单介绍
    当前项目主要是针对社交网站的接口封装，在项目开始主要以微信平台sdk开发为主，根据接口功能分类，归为一下三个组成部分
	
   一. **基本信息模块**  
   
   	用户授权（oauth2.0），用户基础信息
    
   二. **会话消息模块**

	会话管理，接收用户的会话信息，以及对应的响应

   三. **高级功能**

	菜单设置管理，统计管理，模板消息推送等
	
# OS.Social.WX 使用
### 一. 安装使用
   &nbsp;&nbsp;&nbsp;nuget下安装命令：**Install-Package OS.Social.WX**	
### 二. 调用示例
 1. 基础授权调用

``` cs
	    //声明配置信息
	    private static WxAppCoinfig m_Config = new WxAppCoinfig()
        {
            AppSource = "11",
            AppId = "你的appId",
            AppSecret = "你的secretkey"
        };
        // 接口api实例
        private static WxOauthApi m_AuthApi = new WxOauthApi(m_Config);
        
        // 获取微信授权地址
        public ActionResult auth()
        {
            var res = m_AuthApi.GetAuthorizeUrl("http://www.social.com/wxoauth/callback", AuthClientType.WxOffcial);
            return Redirect(res);
        }
        //  微信回调页，此页面获取accesstoken 获取用户基础信息
        public ActionResult callback(string code, string state)
        {
            var tokecRes = m_AuthApi.GetAuthAccessToken(code);
            if (tokecRes.IsSuccess)
            {
                var userInfoRes = m_AuthApi.GetWxAuthUserInfo(tokecRes.AccessToken, tokecRes.OpenId);
                return Content("你已成功获取用户信息!");
            }
            return Content("获取用户授权信息失败!");
        }
```


 	
 2. 会话调用
    a.  首先声明配置信息
``` protobuf
// 声明配置
	private static readonly WxMsgServerConfig config = new WxMsgServerConfig()
        {
            Token = "你的token",
            EncodingAesKey = "你的加密key",
            SecurityType = WxSecurityType.Safe,//  在微信段设置的安全模式
            AppId = "你的appid"   //  
        };
```
b. 定义一个处理句柄（可以实现一个自己的Handler，继承自WxMsgHandler 即可）
``` vbnet
     private static readonly WxMsgHandler msgService = new WxMsgHandler(config);
```
c. 调用时将当前请求的内容传入程序入口即可：
   

``` lasso
           string requestXml;
            using (StreamReader reader = new StreamReader(Request.InputStream))
            {
                requestXml = reader.ReadToEnd();
                LogUtil.Info($"内容 requestXml:{requestXml}");
            }
     var res = msgService.Processing( requestXml, signature, timestamp, nonce);
                if (res.IsSuccess)
                {
                    LogUtil.Info(res.Data);
                    return Content(res.Data);
                }
                LogUtil.Error(res.Message);
```


其中WxMsgHandler 可以是自己继承WxMsgHandler 实现的具体处理类，通过重写相关用户事件返回对应结果即可。

3. 高级功能调用
     
     微信公众号的其他高级功能接口都需要一个全局的accesstoken接口，像推送模块信息等，accesstoken自动获取已经被封装在sdk底层的请求处理中，默认会使用系统缓存保存，过期自动更新，如果需要保存到像redis中可以通过oscommon中的缓存模块注入，添加一个针对sns的缓存模块实现就可以了（后续给出一个示例），access和appid一一对应，不用担心多个公众号的冲突问题。
     
     a.  声明配置信息：

``` d
//声明配置信息
private static WxAppCoinfig m_Config = new WxAppCoinfig()
        {
            AppSource = "11",
            AppId = "你的appId",
            AppSecret = "你的secretkey"
        };
```


   
   b. 声明一个实例：
``` vbnet
    private static readonly WxOffcialApi m_OffcialApi = new WxOffcialApi(m_Config);
```

   c.  具体使用

``` stylus
  m_OffcialApi.SendTemplate("openid","templateId","url",new {})
```


当前这部分接口框架逻辑部分已经处理完毕，具体接口实现有限，后续将很快更新，如果有需要的也可以自己实现或贡献过来，添加一个新接口只需要几行代码即可。

### 三. 实现模式介绍
     尽快完善
     
### 四. 项目依赖
当前项目依赖于我以前写的  通用类库OS.Common(全局错误实体，和模块注入)  和  OS.Http（http请求）   都比较简单小巧，不用担心臃肿
