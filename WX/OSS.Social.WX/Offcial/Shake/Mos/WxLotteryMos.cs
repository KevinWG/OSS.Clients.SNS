#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 摇一摇红包实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-18
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;

namespace OSS.Social.WX.Offcial.Shake.Mos
{
    #region  创建红包活动
    /// <summary>
    ///   创建红包活动请求实体
    /// </summary>
    public class WxAddLotteryReq
    {
        /// <summary>   
        ///    string 抽奖活动名称（选择使用模板时，也作为摇一摇消息主标题），最长6个汉字，12个英文字母。
        /// </summary>  
        public string title { get; set; }

        /// <summary>   
        ///    string 抽奖活动描述（选择使用模板时，也作为摇一摇消息副标题），最长7个汉字，14个英文字母。
        /// </summary>  
        public string desc { get; set; }

        /// <summary>   
        ///    int 抽奖开关。0关闭，1开启，默认为1
        /// </summary>  
        public int onoff { get; set; } = 1;

        /// <summary>   
        ///    long 抽奖活动开始时间，unix时间戳，单位秒
        /// </summary>  
        public long begin_time { get; set; }

        /// <summary>   
        ///    long 抽奖活动结束时间，unix时间戳，单位秒,红包活动有效期最长为91天
        /// </summary>  
        public long expire_time { get; set; }

        /// <summary>   
        ///    string 红包提供商户公众号的appid，需与预下单中的公众账号appid（wxappid）一致
        /// </summary>  
        public string sponsor_appid { get; set; }

        /// <summary>   
        ///    long 红包总数，红包总数是录入红包ticket总数的上限，因此红包总数应该大于等于预下单时红包ticket总数。
        /// </summary>  
        public long total { get; set; }

        /// <summary>   
        ///    string 红包关注界面后可以跳转到第三方自定义的页面
        /// </summary>  
        public string jump_url { get; set; }

        /// <summary>   
        ///    string 开发者自定义的key，用来生成活动抽奖接口的签名参数，长度32位。使用方式见sign生成规则
        /// </summary>  
        public string key { get; set; }


    }
    /// <summary>
    /// 创建红包活动响应实体
    /// </summary>
    public class WxAddLotteryResp : WxBaseResp
    {
        /// <summary>   
        ///    string 生成的红包活动id
        /// </summary>  
        public string lottery_id { get; set; }

        /// <summary>   
        ///    int 生成的模板页面ID
        /// </summary>  
        public int page_id { get; set; }

    }

    #endregion

    #region  设置红包活动对应的红包信息
    /// <summary>
    /// 设置红包活动对应的红包信息请求
    /// </summary>
    public class WxSetLotteryPrizeReq
    {
        /// <summary>   
        ///    string 红包抽奖id，来自addlotteryinfo返回的lottery_id
        /// </summary>  
        public string lottery_id { get; set; }

        /// <summary>   
        ///    string 红包提供者的商户号，，需与预下单中的商户号mch_id一致
        /// </summary>  
        public string mchid { get; set; }

        /// <summary>   
        ///    string 红包提供商户公众号的appid，需与预下单中的公众账号appid（wxappid）一致
        /// </summary>  
        public string sponsor_appid { get; set; }

        /// <summary>   
        ///    json数组 红包ticket列表，如果红包数较多，可以一次传入多个红包，批量调用该接口设置红包信息。每次请求传入的红包个数上限为100
        /// </summary>  
        public List<WxLotteryTicketMo> prize_info_list { get; set; }
    }

    public class WxLotteryTicketMo
    {
        /// <summary>   
        ///    string 预下单时返回的红包ticket，单个活动红包ticket数量上限为100000个，可添加多次。
        /// </summary>  
        public string ticket { get; set; }
    }

    /// <summary>
    /// 设置红包活动对应的红包信息响应实体
    /// </summary>
    public class WxSetLotteryPrizeResp:WxBaseResp
    {
        /// <summary>   
        ///    array 重复使用的ticket列表，如为空，将不返回
        /// </summary>  
        public List<WxLotteryTicketMo> repeat_ticket_list { get; set; }

        /// <summary>   
        ///    array 过期的ticket列表，如为空，将不返回
        /// </summary>  
        public List<WxLotteryTicketMo> expire_ticket_list { get; set; }

        /// <summary>   
        ///    array 金额不在大于1元，小于1000元的ticket列表，如为空，将不返回
        /// </summary>  
        public List<WxLotteryTicketMo> invalid_amount_ticket_list { get; set; }

        /// <summary>   
        ///    int 成功录入的红包数量
        /// </summary>  
        public int success_num { get; set; }

        /// <summary>   
        ///    array 原因：生成红包的时候，授权商户号auth_mchid和auth_appid没有写摇周边的商户号
        /// </summary>  
        public List<WxLotteryTicketMo> wrong_authmchid_ticket_list { get; set; }

        /// <summary>   
        ///    array ticket解析失败，可能有错别字符或不完整
        /// </summary>  
        public List<WxLotteryTicketMo> invalid_ticket_list { get; set; }


    }

    #endregion


    #region 红包查询接口
    /// <summary>
    ///  查询红包响应实体
    /// </summary>
    public class WxGetLotteryResp:WxBaseResp
    {
        /// <summary>
        ///  结果实体
        /// </summary>
        public WxLotteryInfoMo result { get; set; }
    }

    public class WxLotteryInfoMo
    {
        /// <summary>   
        ///    string 抽奖活动名称（选择使用模板时，也作为摇一摇消息主标题），最长6个汉字，12个英文字母。
        /// </summary>  
        public string title { get; set; }

        /// <summary>   
        ///    string 抽奖活动描述（选择使用模板时，也作为摇一摇消息副标题），最长7个汉字，14个英文字母。
        /// </summary>  
        public string desc { get; set; }

        /// <summary>   
        ///    int 抽奖开关。0关闭，1开启，默认为1
        /// </summary>  
        public int onoff { get; set; }

        /// <summary>   
        ///    long 抽奖活动开始时间，unix时间戳，单位秒
        /// </summary>  
        public long begin_time { get; set; }

        /// <summary>   
        ///    long 抽奖活动结束时间，unix时间戳，单位秒，红包活动有效期最长为91天
        /// </summary>  
        public long expire_time { get; set; }

        /// <summary>   
        ///    string 红包提供商户公众号的appid
        /// </summary>  
        public string sponsor_appid { get; set; }

        /// <summary>   
        ///    string 创建活动的开发者appid
        /// </summary>  
        public string appid { get; set; }

        /// <summary>   
        ///    long 已录入的红包总数
        /// </summary>  
        public long prize_count { get; set; }

        /// <summary>   
        ///    long 创建活动时预设的录入红包ticket数量上限
        /// </summary>  
        public long prize_count_limit { get; set; }

        /// <summary>   
        ///    string 红包关注界面后可以跳转到第三方自定义的页面
        /// </summary>  
        public string jump_url { get; set; }

        /// <summary>   
        ///    long 过期红包ticket数量
        /// </summary>  
        public long expired_prizes { get; set; }

        /// <summary>   
        ///    long 已发放的红包ticket数量
        /// </summary>  
        public long drawed_prizes { get; set; }

        /// <summary>   
        ///    long 可用的红包ticket数量
        /// </summary>  
        public long available_prizes { get; set; }

        /// <summary>   
        ///    long 已过期的红包金额总和
        /// </summary>  
        public long expired_value { get; set; }

        /// <summary>   
        ///    long 已发放的红包金额总和
        /// </summary>  
        public long drawed_value { get; set; }

        /// <summary>   
        ///    long 可用的红包金额总和
        /// </summary>  
        public long available_value { get; set; }
    }

    #endregion

}
