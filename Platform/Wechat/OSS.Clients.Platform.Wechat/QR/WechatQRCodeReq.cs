#region Copyright (C) 2017 Kevin (OSS开源实验室) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：微信公众号接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-5
*       
*****************************************************************************/

#endregion

using Newtonsoft.Json;
using System.Net.Http;

namespace OSS.Clients.Platform.Wechat.QR
{
    public class WechatQRCodeReq:WechatBaseTokenReq<WechatQRCodeResp>
    {
        public WechatQRCodeReq(WechatQrCodeBody qrCodeBody) : base(HttpMethod.Post)
        {
            _body = qrCodeBody;
        }

        private WechatQrCodeBody _body;
        public override string GetApiPath()
        {
            return "/cgi-bin/qrcode/create";
        }
        
        protected override void PrepareSend()
        {
            custom_body = JsonConvert.SerializeObject(_body);
        }
    }
    
    /// <summary>
    /// 微信二维码请求
    /// </summary>
    public class WechatQrCodeBody
    {
        /// <summary>
        /// 过期时间，永久二维码请设置为0
        /// </summary>
        public int expire_seconds { get; set; }

        /// <summary>
        /// 生成二维码性质  
        ///  可以通过 typeof(WechatQrCodeType).ToEnumDirs() 获取对应的枚举字典列表
        /// </summary>
        public string action_name { get; set; }

        /// <summary>
        ///   场景二维码信息
        /// </summary>
        public WechatSenceQrMo action_info { get; set; }
    }

    /// <summary>
    /// 二维码场景信息
    /// </summary>
    public class WechatSenceQrMo
    {
        /// <summary>
        ///  场景信息
        /// </summary>
        public WechatSenceQrInfoMo scene;
    }
    
    /// <summary>
    ///   场景二维码生成需要具体实体信息
    /// </summary>
    public class WechatSenceQrInfoMo
    {
        /// <summary>
        /// 场景值id
        /// 场景值ID，临时二维码时为32位非0整型，永久二维码时最大值为100000（目前参数只支持1--100000）
        /// </summary>
        public int scene_id { get; set; }

        /// <summary>
        /// 场景值ID（字符串形式的ID），字符串类型，长度限制为1到64，仅永久二维码支持此字段   
        /// </summary>
        public string scene_str { get; set; }
    }
    
    /// <summary>
    /// 生成二维码ticket
    /// </summary>
    public class WechatQRCodeResp : WechatBaseResp
    {
        /// <summary>   
        ///   获取的二维码ticket，凭借此ticket可以在有效时间内换取二维码。
        /// </summary>  
        public string ticket { get; set; }

        /// <summary>   
        ///   该二维码有效时间，以秒为单位。最大不超过2592000（即30天）。
        /// </summary>  
        public string expire_seconds { get; set; }

        /// <summary>   
        ///   二维码图片解析后的地址，开发者可根据该地址自行生成需要的二维码图片
        /// </summary>  
        public string url { get; set; }

    }
}
