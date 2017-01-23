#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 ——  卡券投放二维码实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-22
*       
*****************************************************************************/

#endregion

using System.ComponentModel;
using Newtonsoft.Json;
using OS.Social.WX.Offcial.Basic.Mos;
using OS.Social.WX.SysUtils.Mos;

namespace OS.Social.WX.Offcial.Card.Mos
{
    /// <summary>
    /// 微信卡券二维码请求
    /// </summary>
    internal class WxCreateCardQrReq
    {
        /// <summary>
        /// 过期时间，永久二维码请设置为0
        /// </summary>
        public int expire_seconds { get; set; }

        /// <summary>
        /// 生成二维码性质   
        /// </summary>
        [JsonConverter(typeof(StringConverter))]
        public WxQrCodeType action_name { get; set; }

        /// <summary>
        ///   场景二维码信息
        /// </summary>
        public object action_info { get; set; }
    }


    public class WxCardQrCodeResp : WxQrCodeResp
    {

        /// <summary>
        /// 二维码显示地址，点击后跳转二维码页面   也可通过ticket接口获取
        /// </summary>
        public string show_qrcode_url { get; set; }
    }

    public class WxCardQrMo
    {
        /// <summary>   
        ///   可空    卡券ID。
        /// </summary>  
        public string card_id { get; set; }

        /// <summary>   
        ///   可空    指定领取者的openid，只有该用户能领取。bind_openid字段为true的卡券必须填写，非指定openid不必填写。
        /// </summary>  
        public string openid { get; set; }

        /// <summary>   
        ///   可空    指定二维码的有效时间，范围是60~1800秒。不填默认为365天有效
        /// </summary>  
        public string expire_seconds { get; set; }

        /// <summary>   
        ///   可空    指定下发二维码，生成的二维码随机分配一个code，领取后不可再次扫描。填写true或false。默认false，注意填写该字段时，卡券须通过审核且库存不为0。
        /// </summary>  
        public string is_unique_code { get; set; }

        /// <summary>   
        ///   可空    领取场景值，用于领取渠道的数据统计，默认值为0，字段类型为整型，长度限制为60位数字。用户领取卡券后触发的事件推送中会带上此自定义场景值。
        /// </summary>  
        public string outer_id { get; set; }

        /// <summary>   
        ///   可空    outer_id字段升级版本，字符串类型，用户首次领卡时，会通过领取事件推送给商户；对于会员卡的二维码，用户每次扫码打开会员卡后点击任何url，会将该值拼入url中，方便开发者定位扫码来源
        /// </summary>  
        public string outer_str { get; set; }
    }
}
