#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 卡券核销接口对应实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-22
*       
*****************************************************************************/

#endregion

using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OSS.Social.WX.Offcial.Card.Mos
{




    #region  查询code核销状态实体   

    public class WxGetCardCodeConsumeResp : WxBaseResp
    {
        /// <summary>   
        ///   用户openid
        /// </summary>  
        public string openid { get; set; }
        
        /// <summary>   
        ///   卡券核销信息
        /// </summary>  
        public WxCardTimeItemMo card { get; set; }

        /// <summary>   
        ///   当前code对应卡券的状态，NORMAL 正常  CONSUMED 已核销  EXPIRE 已过期
        /// GIFTING 转赠中 GIFT_TIMEOUT 转赠超时   DELETE 已删除，UNAVAILABLE 已失效    
        /// code未被添加或被转赠领取的情况则统一报错：invalidserialcode
        /// 可以通过 typeof(WxCardCodeUseState).ToEnumDirs() 获取对应的枚举字典列表
        /// </summary>  
        public string user_card_status { get; set; }

        /// <summary>   
        ///   是否可以核销，true为可以核销，false为不可核销
        /// </summary>  
        public bool can_consume { get; set; }
    }


    public class WxCardTimeItemMo
    {
        /// <summary>   
        ///   卡券ID
        /// </summary>  
        public string card_id { get; set; }

        /// <summary>    
        ///   起始使用时间   应该是东区时间 待测试
        /// </summary>  
        public long begin_time { get; set; }

        /// <summary>   
        ///   结束时间   应该是东区时间 待测试
        /// </summary>  
        public long end_time { get; set; }
    }

    #endregion


    #region  核销code

    /// <summary>
    ///   核销code时响应的实体
    /// </summary>
    public class WxCardConsumeResp:WxBaseResp
    {
        /// <summary>
        /// 用户在该公众号内的唯一身份标识
        /// </summary>
        public string openid { get; set; }

        /// <summary>
        ///   卡券ID
        /// </summary>
        public string card_id { get; set; }

        /// <summary>
        ///   内部包含card_id ，请直接使用 card_id 字段
        /// </summary>
        internal object card {
            set
            {
                var _card = value;
                card_id = _card == null ? string.Empty : ((JToken) _card)["card_id"].ToString();
            }
        }
    }

    #endregion


    #region  线上核销需要的

    /// <summary>
    ///  code 解密接口
    /// </summary>
    public class WxCardCodeDecryptResp:WxBaseResp
    {
        /// <summary>
        /// 解密后的code码
        /// </summary>
        public string code { get; set; }
    }

    #endregion

}
