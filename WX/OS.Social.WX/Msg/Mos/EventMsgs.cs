using OS.Common.Extention;

namespace OS.Social.WX.Msg.Mos
{
    /// <summary>
    /// 关注/取消关注/扫描带参数二维码事件
    /// </summary>
    public class SubscribeRecEventMsg : BaseRecEventMsg
    {
        protected override void FormatPropertiesFromMsg()
        {
            base.FormatPropertiesFromMsg();

            EventKey = this["EventKey"];
            Ticket = this["Ticket"];
        }

        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }


    }

    /// <summary>
    /// 上报地理位置事件
    /// </summary>
    public class LocationRecEventMsg : BaseRecEventMsg
    {
        protected override void FormatPropertiesFromMsg()
        {
            base.FormatPropertiesFromMsg();

            Latitude = this["Latitude"].ToDouble();
            Longitude = this["Longitude"].ToDouble();
            Precision = this["Precision"].ToDouble();
        }

        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 地理位置精度
        /// </summary>
        public double Precision { get; set; }

    }

    /// <summary>
    /// 点击菜单拉取消息时的事件推送
    /// </summary>
    public class ClickRecEventMsg : BaseRecEventMsg
    {
        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            base.FormatPropertiesFromMsg();
            EventKey = this["EventKey"];
        }

        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }

    }

    /// <summary>
    /// 点击菜单跳转链接时的事件推送 
    /// </summary>
    public class ViewRecEventMsg : BaseRecEventMsg
    {
        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            base.FormatPropertiesFromMsg();
            EventKey = this["EventKey"];
        }

        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }

    }


}
