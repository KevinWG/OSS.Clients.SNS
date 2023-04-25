namespace OSS.Clients.MApp.Wechat
{
    public class WechatMAppUserInfo
    {
        public string openId   { get; set; }
        public string nickName { get; set; }

        public string unionId { get; set; }

        public int    gender   { get; set; }
        public string language { get; set; }
        public string city     { get; set; }
        public string province { get; set; }

        public string country   { get; set; }
        public string avatarUrl { get; set; }
    }
}