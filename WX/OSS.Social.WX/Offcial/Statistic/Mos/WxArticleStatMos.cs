#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 图文统计接口实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-19
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Generic;

namespace OSS.Social.WX.Offcial.Statistic.Mos
{

    /// <summary>
    ///   文章分享统计实体基类
    /// </summary>
    public class WxArticleStatBaseMo
    {
        /// <summary>   
        ///   分享的人数
        /// </summary>  
        public int share_user { get; set; }

        /// <summary>   
        ///   分享的次数
        /// </summary>  
        public int share_count { get; set; }
        
    }
    /// <summary>
    ///   文章分享统计 每篇文章每个子项实体基类
    /// </summary>
    public class WxArticleStatItemBaseMo : WxArticleStatBaseMo
    {
        /// <summary>   
        ///   图文页（点击群发图文卡片进入的页面）的阅读人数
        /// </summary>  
        public int int_page_read_user { get; set; }

        /// <summary>   
        ///   图文页的阅读次数
        /// </summary>  
        public int int_page_read_count { get; set; }

        /// <summary>   
        ///   原文页（点击图文页“阅读原文”进入的页面）的阅读人数，无原文页时此处数据为0
        /// </summary>  
        public int ori_page_read_user { get; set; }

        /// <summary>   
        ///   原文页的阅读次数
        /// </summary>  
        public int ori_page_read_count { get; set; }

        /// <summary>   
        ///   收藏的人数
        /// </summary>  
        public int add_to_fav_user { get; set; }

        /// <summary>   
        ///   收藏的次数
        /// </summary>  
        public int add_to_fav_count { get; set; }
    }


    /// <summary>
    /// 每日 群发图文（文章组合）统计  单篇文章的数据
    /// </summary>
    public class WxArticleStatSendMo : WxArticleStatItemBaseMo
    {
        /// <summary>   
        ///   数据的日期，需在begin_date和end_date之间
        /// </summary>  
        public DateTime ref_date { get; set; }

        /// <summary>   
        ///   请注意：这里的msgid实际上是由msgid（图文消息id，这也就是群发接口调用后返回的msg_data_id）和index（消息次序索引）组成，例如12003_3，其中12003是msgid，即一次群发的消息的id；3为index，假设该次群发的图文消息共5个文章（因为可能为多图文），3表示5个中的第3个
        /// </summary>  
        public string msgid { get; set; }

        /// <summary>   
        ///   图文消息的标题
        /// </summary>  
        public string title { get; set; }
    }
    
    /// <summary>
    /// 总统计中 每日群发的文章组合中 单篇文章连续多日的统计信息
    /// </summary>
    public class WxArticleStatSendTotalMo 
    {
        /// <summary>   
        ///   数据的日期，需在begin_date和end_date之间
        /// </summary>  
        public DateTime ref_date { get; set; }

        /// <summary>   
        ///   请注意：这里的msgid实际上是由msgid（图文消息id，这也就是群发接口调用后返回的msg_data_id）和index（消息次序索引）组成，例如12003_3，其中12003是msgid，即一次群发的消息的id；3为index，假设该次群发的图文消息共5个文章（因为可能为多图文），3表示5个中的第3个
        /// </summary>  
        public string msgid { get; set; }

        /// <summary>   
        ///   图文消息的标题
        /// </summary>  
        public string title { get; set; }

        /// <summary>
        /// 当前文章最多七天的统计信息
        /// </summary>
        public List<WxArticleStatSendTotalItemMo> details { get; set; }
    }

    /// <summary>
    /// 总统计中 每日群发的文章组合中 单篇文章下的 单天的统计数据细节
    /// </summary>
    public class WxArticleStatSendTotalItemMo : WxArticleStatItemBaseMo
    {
        /// <summary>   
        ///   统计的日期，在getarticletotal接口中，ref_date指的是文章群发出日期，而stat_date是数据统计日期
        /// </summary>  
        public DateTime stat_date { get; set; }

        /// <summary>   
        ///   送达人数，一般约等于总粉丝数（需排除黑名单或其他异常情况下无法收到消息的粉丝）
        /// </summary>  
        public int target_user { get; set; }

        /// <summary>   
        ///   公众号会话阅读人数
        /// </summary>  
        public int intpagefromsessionreaduser { get; set; }

        /// <summary>   
        ///   公众号会话阅读次数
        /// </summary>  
        public int intpagefromsessionreadcount { get; set; }

        /// <summary>   
        ///   历史消息页阅读人数
        /// </summary>  
        public int intpagefromhistmsgreaduser { get; set; }

        /// <summary>   
        ///   历史消息页阅读次数
        /// </summary>  
        public int intpagefromhistmsgreadcount { get; set; }

        /// <summary>   
        ///   朋友圈阅读人数
        /// </summary>  
        public int intpagefromfeedreaduser { get; set; }

        /// <summary>   
        ///   朋友圈阅读次数
        /// </summary>  
        public int intpagefromfeedreadcount { get; set; }

        /// <summary>   
        ///   好友转发阅读人数
        /// </summary>  
        public int intpagefromfriendsreaduser { get; set; }

        /// <summary>   
        ///   好友转发阅读次数
        /// </summary>  
        public int intpagefromfriendsreadcount { get; set; }

        /// <summary>   
        ///   其他场景阅读人数
        /// </summary>  
        public int intpagefromotherreaduser { get; set; }

        /// <summary>   
        ///   其他场景阅读次数
        /// </summary>  
        public int intpagefromotherreadcount { get; set; }

        /// <summary>   
        ///   公众号会话转发朋友圈人数
        /// </summary>  
        public int feedsharefromsessionuser { get; set; }

        /// <summary>   
        ///   公众号会话转发朋友圈次数
        /// </summary>  
        public int feedsharefromsessioncnt { get; set; }

        /// <summary>   
        ///   朋友圈转发朋友圈人数
        /// </summary>  
        public int feedsharefromfeeduser { get; set; }

        /// <summary>   
        ///   朋友圈转发朋友圈次数
        /// </summary>  
        public int feedsharefromfeedcnt { get; set; }

        /// <summary>   
        ///   其他场景转发朋友圈人数
        /// </summary>  
        public int feedsharefromotheruser { get; set; }

        /// <summary>   
        ///   其他场景转发朋友圈次数
        /// </summary>  
        public int feedsharefromothercnt { get; set; }
    }


    /// <summary>
    ///  每天的统计
    /// </summary>
    public class WxArticleStatDaliyMo : WxArticleStatItemBaseMo
    {
        /// <summary>   
        ///   数据的日期，需在begin_date和end_date之间
        /// </summary>  
        public DateTime ref_date { get; set; }
    }

    /// <summary>
    ///  每天的统计
    /// </summary>
    public class WxArticleStatHourMo : WxArticleStatDaliyMo
    {
        /// <summary>   
        ///   数据的小时，包括从000到2300，分别代表的是[000,100)到[2300,2400)，即每日的第1小时和最后1小时
        /// </summary>  
        public int ref_hour { get; set; }


        /// <summary>   
        ///   在获取图文阅读分时数据时才有该字段，代表用户从哪里进入来阅读该图文。0:会话;1.好友;2.朋友圈;3.腾讯微博;4.历史消息页;5.其他
        /// </summary>  
        public int user_source { get; set; }
    }


    /// <summary>
    ///   文章分享统计实体
    ///    不同share_scene（分享场景）的数据，以及ref_date在begin_date和end_date之间的数据
    /// </summary>
    public class WxArticleStatShareMo:WxArticleStatBaseMo
    {  
        /// <summary>   
        ///   数据的日期，需在begin_date和end_date之间
        /// </summary>  
        public DateTime ref_date { get; set; }

        /// <summary>   
        ///   分享的场景1代表好友转发
        /// 2代表朋友圈3代表腾讯微博255代表其他
        /// </summary>  
        public int share_scene { get; set; }
    }
    
    /// <summary>
    ///   文章分享每日统计实体
    /// </summary>
    public class WxArticleStatShareHourMo: WxArticleStatShareMo
    {
        /// <summary>   
        ///   数据的小时，包括从000到2300，分别代表的是[000,100)到[2300,2400)，即每日的第1小时和最后1小时
        /// </summary>  
        public int ref_hour { get; set; }
    }
}
