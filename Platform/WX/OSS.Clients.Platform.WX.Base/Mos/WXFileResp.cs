namespace OSS.Clients.Platform.WX.Base.Mos
{
    /// <summary>
    /// 文件下载的响应实体
    /// </summary>
    public class WXFileResp : WXBaseResp
    {
        /// <summary>
        ///  请求中的contentteype
        /// </summary>
        public string content_type { get; set; }

        /// <summary>
        ///  文件的字节流
        /// </summary>
        public byte[] file { get; set; }
    }
}
