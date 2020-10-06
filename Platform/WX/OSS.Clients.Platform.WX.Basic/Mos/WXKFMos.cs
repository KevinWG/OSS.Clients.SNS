#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscore

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 客服管理接口实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-2
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using OSS.Clients.Platform.WX.Base.Mos;

namespace OSS.Clients.Platform.WX.Basic.Mos
{
    #region  客服账号信息实体

    /// <summary>
    ///   获取客服列表响应实体
    /// </summary>
    public class WXGetKFAccountListResp : WXBaseResp
    {
        public List<WXKFInfoMo> kf_list { get; set; }
    }

    public class WXKFInfoMo
    {
        /// <summary>   
        ///    完整客服账号，格式为：账号前缀@公众号微信号
        /// </summary>  
        public string kf_account { get; set; }

        /// <summary>   
        ///    客服昵称
        /// </summary>  
        public string kf_nick { get; set; }

        /// <summary>   
        ///     客服工号
        /// </summary>  
        public string kf_id { get; set; }

        /// <summary>   
        ///    客服头像
        /// </summary>  
        public string kf_headimgurl { get; set; }

        /// <summary>   
        ///    如果客服帐号已绑定了客服人员微信号，则此处显示微信号
        /// </summary>  
        public string kf_wx { get; set; }

        /// <summary>   
        ///    如果客服帐号尚未绑定微信号，但是已经发起了一个绑定邀请，则此处显示绑定邀请的微信号
        /// </summary>  
        public string invite_wx { get; set; }

        /// <summary>   
        ///    如果客服帐号尚未绑定微信号，但是已经发起过一个绑定邀请，邀请的过期时间，为unix 时间戳
        /// </summary>  
        public long invite_expire_time { get; set; }

        /// <summary>   
        ///    邀请的状态，有等待确认“waiting”，被拒绝“rejected”，过期“expired”
        /// </summary>  
        public string invite_status { get; set; }



    }

    #endregion
    
    #region  在线客服信息

    /// <summary>
    ///  获取在线客服响应实体
    /// </summary>
    public class WXGetKfOnlineResp : WXBaseResp
    {
        /// <summary>
        /// 在线列表
        /// </summary>
        public List<WXKfOnlineMo> kf_online_list { get; set; }
    }


    /// <summary>
    ///   客服在线信息实体
    /// </summary>
    public class WXKfOnlineMo
    {
        /// <summary>   
        ///    完整客服帐号，格式为：帐号前缀@公众号微信号
        /// </summary>  
        public string kf_account { get; set; }

        /// <summary>   
        ///    客服在线状态，目前为：1、web 在线
        /// </summary>  
        public int status { get; set; }

        /// <summary>   
        ///    客服编号
        /// </summary>  
        public string kf_id { get; set; }

        /// <summary>   
        ///    客服当前正在接待的会话数
        /// </summary>  
        public int accepted_case { get; set; }
    }

    #endregion

    #region  会话实体
    /// <summary>
    /// 获取用户客服会话响应实体
    /// </summary>
    public class WXGetUserKfSessionResp:WXBaseResp
    {
        /// <summary>   
        ///    正在接待的客服，为空表示没有人在接待
        /// </summary>  
        public string kf_account { get; set; }

        /// <summary>   
        ///    会话接入的时间
        /// </summary>  
        public long createtime { get; set; }
    }

    /// <summary>
    ///  客服会话对应的用户信息
    /// </summary>
    public class WXKfSessionUserMo
    {
        /// <summary>   
        ///    会话接入的时间
        /// </summary>  
        public long createtime { get; set; }

        /// <summary>
        ///   用户openid
        /// </summary>
        public string openid { get; set; }
    }
    /// <summary>
    ///  获取客服对应的用户会话列表响应实体
    /// </summary>
    public class WXGetKfSessionsByAccountResp:WXBaseResp
    {
        /// <summary>
        /// 当前客服对应的会话列表
        /// </summary>
        public List<WXKfSessionUserMo> sessionlist { get; set; }
    }
    /// <summary>
    ///   客服等待用户信息实体
    /// </summary>
    public class WXKfWaitUserMo
    {
        /// <summary>   
        ///    粉丝的最后一条消息的时间
        /// </summary>  
        public long latest_time { get; set; }

        /// <summary>
        ///   用户openid
        /// </summary>
        public string openid { get; set; }
    }
    /// <summary>
    ///  获取等待接入会话用户列表响应实体
    /// </summary>
    public class WXGetKfWaitUserListResp:WXBaseResp
    {
        /// <summary>
        ///  等待的数量
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 等待用户列表
        /// </summary>
        public List<WXKfWaitUserMo> waitcaselist { get; set; }
    }

    #endregion

    #region  聊天记录

    public class WXGetKfMsgListReq
    {
        /// <summary>   
        ///    起始时间，unix时间戳
        /// </summary>  
        public long starttime { get; set; }

        /// <summary>   
        ///    结束时间，unix时间戳，每次查询时段不能超过24小时
        /// </summary>  
        public long  endtime { get; set; }

        /// <summary>   
        ///    消息id顺序从小到大，从1开始
        /// </summary>  
        public long msgid { get; set; }

        /// <summary>   
        ///    每次获取条数，最多10000条
        /// </summary>  
        public int number { get; set; }

    }

    /// <summary>
    ///  获取客服聊天记录
    /// </summary>
    public class WXGetKfMsgListResp:WXBaseResp
    {
        /// <summary>
        ///  聊天记录列表
        /// </summary>
        public List<WXKfMsgItemMo> recordlist { get; set; }

        /// <summary>
        ///  数量
        /// </summary>
        public int number { get; set; }

        /// <summary>
        ///  消息id
        /// </summary>
        public long msgid { get; set; }
    }

    /// <summary>
    /// 客服消息实体
    /// </summary>
    public class WXKfMsgItemMo
    {
        /// <summary>   
        ///    完整客服帐号，格式为：帐号前缀@公众号微信号
        /// </summary>  
        public string worker { get; set; }

        /// <summary>   
        ///    用户标识
        /// </summary>  
        public string openid { get; set; }

        /// <summary>   
        ///    操作码，2002（客服发送信息），2003（客服接收消息）
        /// </summary>  
        public int opercode { get; set; }

        /// <summary>   
        ///    聊天记录
        /// </summary>  
        public string text { get; set; }

        /// <summary>   
        ///    操作时间，unix时间戳
        /// </summary>  
        public long time { get; set; }
    }

    #endregion
}
