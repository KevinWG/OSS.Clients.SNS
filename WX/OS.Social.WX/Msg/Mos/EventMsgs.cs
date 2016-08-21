using System;

namespace OS.Social.WX.Msg.Mos
{
    /// <summary>
    /// 关注/取消关注/扫描带参数二维码事件
    /// </summary>
    public class SubscribeEvent : BaseRecEventContext
    {
        protected override void FormatProperties()
        {
            base.FormatProperties();

            EventKey = GetValue("EventKey");
            Ticket = GetValue("Ticket");
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
    public class LocationEvent : BaseRecEventContext
    {
        protected override void FormatProperties()
        {
            base.FormatProperties();

            Latitude = Convert.ToDouble(GetValue("Latitude"));
            Longitude = Convert.ToDouble(GetValue("Longitude"));
            Precision = Convert.ToDouble(GetValue("Precision"));
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
    public class ClickEvent : BaseRecEventContext
    {
        protected override void FormatProperties()
        {
            base.FormatProperties();
            EventKey = GetValue("EventKey");
        }

        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }
    }

    /// <summary>
    /// 点击菜单跳转链接时的事件推送 
    /// </summary>
    public class ViewEvent : BaseRecEventContext
    {
        protected override void FormatProperties()
        {
            base.FormatProperties();
            EventKey = GetValue("EventKey");
        }

        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }
    }

    /// <summary>
    /// 客服事件
    /// </summary>
    public class KFEvent : BaseRecEventContext
    {
        protected override void FormatProperties()
        {
            base.FormatProperties();
            KfAccount = GetValue("KfAccount");
        }

        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string KfAccount { get; set; }
    }
}
