namespace OS.Social.WX.Msg.Mos
{
    /// <summary>
    /// 回复消息类型
    /// </summary>
    public enum ReplyMsgType
    {
        /// <summary>
        /// 不在以列事件类型枚举中，开发者自己从   recMsg["columnname"]  中获取指定字段的值
        /// </summary>
        None = 0,

        /// <summary>
        /// 文本消息
        /// </summary>
        Text = 1,

        /// <summary>
        /// 图片消息
        /// </summary>
        Image = 2,

        /// <summary>
        /// 语音消息
        /// </summary>
        Voice = 3,

        /// <summary>
        /// 视频消息
        /// </summary>
        Video = 4,

        /// <summary>
        /// 音乐消息
        /// </summary>
        Music = 5,

        /// <summary>
        /// 图文消息
        /// </summary>
        News = 6,

        /// <summary>
        /// 转到客服
        /// </summary>
        KeFu = 10
    }

    /// <summary>
    ///  消息类型
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        ///  不在以列消息类型枚举中，开发者自己从   recMsg["columnname"]  中获取指定字段的值
        /// </summary>
        None = 0,

        /// <summary>
        /// 事件
        /// </summary>
        Event = 1,

        /// <summary>
        /// 文本
        /// </summary>
        Text = 2,

        /// <summary>
        /// 图片
        /// </summary>
        Image = 3,

        /// <summary>
        /// 语音
        /// </summary>
        Voice = 4,

        /// <summary>
        /// 视频
        /// </summary>
        Video = 5,

        /// <summary>
        /// 小视频
        /// </summary>
        Shortvideo = 6,

        /// <summary>
        /// 地理位置
        /// </summary>
        Location = 7,

        /// <summary>
        /// 链接
        /// </summary>
        Link = 8
    }

    /// <summary>
    /// 事件类型
    /// </summary>
    public enum EventType
    {
        /// <summary>
        ///  未定义事件类型
        /// </summary>
        None = 0,

        /// <summary>
        /// 关注事件/扫描带参数二维码事件(用户未关注时，进行关注后的事件推送)
        /// </summary>
        Subscribe = 1,

        /// <summary>
        /// 取消关注事件
        /// </summary>
        UnSubscribe = 2,

        /// <summary>
        /// 扫描带参数二维码事件(用户已关注时的事件推送)
        /// </summary>
        Scan = 3,

        /// <summary>
        /// 自定义菜单事件
        /// </summary>
        Click = 4,

        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        Location = 5,

        /// <summary>
        /// 点击菜单跳转链接时的事件推送
        /// </summary>
        View = 6,

        /// <summary>
        /// 客服事件
        /// </summary>
        Kefu = 7
    }
}
