#region Copyright (C) 2017   Kevin   （OS系列开源项目）

/***************************************************************************
*　　	文件功能描述: 公号的功能辅助接口 —— js相关接口实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-24
*       
*****************************************************************************/

#endregion

using System;
using Newtonsoft.Json;
using OSS.Common.BasicMos.Resp;

namespace OSS.Clients.Platform.WX.Assist.Mos
{
    public class WXGetJsTicketResp : WXBaseResp
    {
        /// <summary>   
        ///   签名所需凭证
        /// </summary>  
        public string ticket { get; set; }

        /// <summary>   
        ///   有效时间
        /// </summary>  
        public int expires_in { get; set; }

        /// <summary>
        ///   过期时间
        /// </summary>
        [JsonIgnore]
        public DateTime expires_time { get; set; }
    }


    /// <summary>
    ///  jssdk 需要的签名信息
    /// </summary>
    public class WXJsSdkSignatureResp:Resp
    {
        /// <summary>
        ///  应用Id
        /// </summary>
        public string app_id { get; set; }

        /// <summary>
        ///  随机串
        /// </summary>
        public string noncestr { get; set; }

        /// <summary>
        ///  随机串
        /// </summary>
        public long timestamp { get; set; }

        /// <summary>
        /// 签名信息
        /// </summary>
        public string signature { get; set; }
    }

    /// <summary>
    ///  JsCard 需要的签名信息
    /// </summary>
    public class WXJsCardSignatureResp:Resp
    {
        /// <summary>
        ///  门店ID。shopID用于筛选出拉起带有指定location_list(shopID)的卡券列表，非必填。
        /// </summary>
        public string shop_id { get; set; }

        /// <summary>
        ///  卡券类型，用于拉起指定卡券类型的卡券列表。当cardType为空时，默认拉起所有卡券的列表，非必填。
        /// </summary>
        public string card_type { get; set; }


        /// <summary>
        ///  卡券ID，用于拉起指定cardId的卡券列表，当cardId为空时，默认拉起所有卡券的列表，非必填。
        /// </summary>
        public string card_id { get; set; }

        /// <summary>
        ///  随机串
        /// </summary>
        public string noncestr { get; set; }

        /// <summary>
        ///  随机串
        /// </summary>
        public long timestamp { get; set; }


        /// <summary>
        ///  签名方式，目前仅支持SHA1。
        /// </summary>
        public string sign_type { get; set; } = "SHA1";

        /// <summary>
        /// 签名信息
        /// </summary>
        public string card_sign { get; set; }

    }

}
