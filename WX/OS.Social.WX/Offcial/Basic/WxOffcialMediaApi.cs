#region Copyright (C) 2017   Kevin   （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述: 公号素材管理部分
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-16
*       
*****************************************************************************/

#endregion

using OS.Http;
using OS.Http.Models;
using OS.Social.WX.Offcial.Basic.Mos;

namespace OS.Social.WX.Offcial.Basic
{
     public partial class WxOffcialApi
    {
        /// <summary>
        /// 上传素材接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
         public WxMediaUploadResp UploadMedia(WxMediaUploadReq request )
         {
            var req=new OsHttpRequest();
            req.HttpMothed=HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl, "/cgi-bin/media/upload?type=", request.type.ToString());
            req.FileParameterList.Add(new FileParameter(request.name, request.file_stream,request.file_name,request.content_type));

            return RestCommonOffcial<WxMediaUploadResp>(req);
         }
    }
}
