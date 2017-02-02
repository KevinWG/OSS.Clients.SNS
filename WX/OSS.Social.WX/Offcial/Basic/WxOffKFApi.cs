#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 客服管理接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-2
*       
*****************************************************************************/

#endregion

using OSS.Http;
using OSS.Http.Models;
using OSS.Social.WX.Offcial.Basic.Mos;

namespace OSS.Social.WX.Offcial.Basic
{
   public partial class WxOffBasicApi
   {
       /// <summary>
       ///   添加客服账号
       /// </summary>
       /// <param name="account">完整客服账号，格式为：账号前缀@公众号微信号</param>
       /// <param name="nickname">客服昵称，最长6个汉字或12个英文字符</param>
       /// <param name="password">客服账号登录密码，格式为密码明文的32位加密MD5值。该密码仅用于在公众平台官网的多客服功能中使用，若不使用多客服功能，则不必设置密码</param>
       /// <returns></returns>
       public WxBaseResp AddKFAccount(string account, string nickname, string password)
       {
           var req = new OsHttpRequest();
           req.HttpMothed = HttpMothed.POST;
           req.AddressUrl = string.Concat(m_ApiUrl, "/customservice/kfaccount/add");
           req.CustomBody = $"{{\"kf_account\":\"{account}\",\"nickname\":\"{nickname}\",\"password\":\"{password}\"}}";

           return RestCommonOffcial<WxBaseResp>(req);
       }


        /// <summary>
        ///   修改客服账号
        /// </summary>
        /// <param name="account">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="nickname">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="password">客服账号登录密码，格式为密码明文的32位加密MD5值。该密码仅用于在公众平台官网的多客服功能中使用，若不使用多客服功能，则不必设置密码</param>
        /// <returns></returns>
        public WxBaseResp UpdateKFAccount(string account, string nickname, string password)
        {
            var req = new OsHttpRequest();
            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/customservice/kfaccount/update");
            req.CustomBody = $"{{\"kf_account\":\"{account}\",\"nickname\":\"{nickname}\",\"password\":\"{password}\"}}";

            return RestCommonOffcial<WxBaseResp>(req);
        }


       /// <summary>
       ///   设置客服账号头像
       /// </summary>
       /// <param name="account">完整客服账号，格式为：账号前缀@公众号微信号</param>
       /// <param name="fileReq">头像的文件信息</param>
       /// <returns></returns>
       public WxBaseResp UploadKFHeadImg(string account, WxFileReq fileReq)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/customservice/kfaccount/uploadheadimg?kf_account=", account);
            req.FileParameterList.Add(new FileParameter("media",fileReq.file_stream,fileReq.file_name,fileReq.content_type));

            return RestCommonOffcial<WxBaseResp>(req);
        }

        /// <summary>
        ///  获取客服列表
        /// </summary>
        /// <returns></returns>
       public WxGetKFAccountListResp GetKFAccountList()
       {
            var req = new OsHttpRequest();
            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/customservice/getkflist");
         
            return RestCommonOffcial<WxGetKFAccountListResp>(req);
        }

   }
}
