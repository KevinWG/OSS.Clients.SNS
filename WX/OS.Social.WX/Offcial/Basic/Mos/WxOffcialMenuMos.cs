using System.Collections.Generic;

namespace OS.Social.WX.Offcial.Basic.Mos
{
    /// <summary>
    /// 微信菜单按钮实体
    /// </summary>
    public class WxMenuButtonInfo
    {
        /// <summary>   
        ///   必填    一级菜单数组，个数应为1~3个
        /// </summary>  
        public string button { get; set; }

        /// <summary>   
        ///   可空    二级菜单数组，个数应为1~5个
        /// </summary>  
        public List<WxMenuButtonInfo> sub_button { get; set; }

        /// <summary>   
        ///   必填    菜单的响应动作类型
        /// </summary>  
        public WxButtonType type { get; set; }

        /// <summary>   
        ///   必填    菜单标题，不超过16个字节，子菜单不超过60个字节
        /// </summary>  
        public string name { get; set; }

        /// <summary>   
        ///   click等点击类型必须菜单KEY值，用于消息接口推送，不超过128字节
        /// </summary>  
        public string key { get; set; }

        /// <summary>   
        ///   view类型必须网页链接，用户点击菜单可打开链接，不超过1024字节
        /// </summary>  
        public string url { get; set; }

        /// <summary>   
        ///   media_id类型和view_limited类型必须调用新增永久素材接口返回的合法media_id
        /// </summary>  
        public string media_id { get; set; }



    }



    /// <summary>
    ///  微信菜单按钮的类型
    /// </summary>
    public enum WxButtonType
    {
        /// <summary>
        /// 点击推事件用户点击click类型按钮后，微信服务器会通过消息接口推送消息类型为event的结构给开发者（参考消息接口指南），并且带上按钮中开发者填写的key值，开发者可以通过自定义的key值与用户进行交互；
        /// </summary>
        click,
        /// <summary>
        /// 跳转URL用户点击view类型按钮后，微信客户端将会打开开发者在按钮中填写的网页URL，可与网页授权获取用户基本信息接口结合，获得用户基本信息。
        /// </summary>
        view,

        /// <summary>
        /// 扫码推事件用户点击按钮后，微信客户端将调起扫一扫工具，完成扫码操作后显示扫描结果（如果是URL，将进入URL），且会将扫码的结果传给开发者，开发者可以下发消息。
        /// </summary>
        scancode_push,

        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框用户点击按钮后，微信客户端将调起扫一扫工具，完成扫码操作后，将扫码的结果传给开发者，同时收起扫一扫工具，然后弹出“消息接收中”提示框，随后可能会收到开发者下发的消息。
        /// </summary>
        scancode_waitmsg,

        /// <summary>
        /// 弹出系统拍照发图用户点击按钮后，微信客户端将调起系统相机，完成拍照操作后，会将拍摄的相片发送给开发者，并推送事件给开发者，同时收起系统相机，随后可能会收到开发者下发的消息。
        /// </summary>
        pic_sysphoto,

        /// <summary>
        /// 弹出拍照或者相册发图用户点击按钮后，微信客户端将弹出选择器供用户选择“拍照”或者“从手机相册选择”。用户选择后即走其他两种流程。
        /// </summary>
        pic_photo_or_album,

        /// <summary>
        /// 弹出微信相册发图器用户点击按钮后，微信客户端将调起微信相册，完成选择操作后，将选择的相片发送给开发者的服务器，并推送事件给开发者，同时收起相册，随后可能会收到开发者下发的消息。
        /// </summary>
        pic_weixin,

        /// <summary>
        /// 弹出地理位置选择器用户点击按钮后，微信客户端将调起地理位置选择工具，完成选择操作后，将选择的地理位置发送给开发者的服务器，同时收起位置选择工具，随后可能会收到开发者下发的消息。
        /// </summary>
        location_select,


        
        /// <summary>
        /// 下发消息（除文本消息）用户点击media_id类型按钮后，微信服务器会将开发者填写的永久素材id对应的素材下发给用户，永久素材类型可以是图片、音频、视频、图文消息。请注意：永久素材id必须是在“素材管理/新增永久素材”接口上传后获得的合法id。
        /// </summary>
        media_id,

        /// <summary>
        /// 跳转图文消息URL用户点击view_limited类型按钮后，微信客户端将打开开发者在按钮中填写的永久素材id对应的图文消息URL，永久素材类型只支持图文消息。请注意：永久素材id必须是在“素材管理/新增永久素材”接口上传后获得的合法id。
        /// </summary>
        view_limited,

    }
}
