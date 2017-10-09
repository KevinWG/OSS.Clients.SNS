using System.Collections.Generic;
using OSS.SnsSdk.Official.Wx.SysTools.Mos;

namespace OSS.SnsSdk.Official.Wx.Basic.Mos
{
    /// <summary>
    /// 微信菜单按钮实体
    /// </summary>
    public class WxMenuButtonMo
    {
        /// <summary>   
        ///   可空    二级菜单数组，个数应为1~5个
        /// </summary>  
        public List<WxMenuButtonMo> sub_button { get; set; }

        /// <summary>   
        ///   必填    菜单的响应动作类型
        /// typeof(WxButtonType).ToEnumDirs()  可获取字典信息
        /// </summary>  
        public string type { get; set; }

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
    ///   微信菜单查询响应实体
    /// </summary>

    public class WxGetMenuResp:WxBaseResp
    {
        /// <summary>
        /// 默认按钮信息
        /// </summary>
        public WxMenuButtonItemsMo menu { get; set; }

        /// <summary>
        ///  个性化菜单分组
        /// </summary>
        public List<WxMenuButtonItemsMo> conditionalmenu { get; set; }
    }

    /// <summary>
    ///   菜单中的按钮数组
    /// </summary>
    public class WxMenuButtonItemsMo
    {
       /// <summary>
       /// 按钮列表
       /// </summary>
        public List<WxMenuButtonMo> button { get; set; }


        /// <summary>
        /// menuid为菜单id
        /// </summary>
        public long menuid { get; set; }


        /// <summary>
        ///  菜单组的匹配规则
        /// </summary>
        public WxMenuMatchRuleMo matchrule { get; set; }
    }

    /// <summary>
    /// 个性化菜单匹配规则
    /// </summary>
    public class WxMenuMatchRuleMo
    {
        /// <summary>   
        ///   可空    用户标签的id，可通过用户标签管理接口获取
        /// </summary>  
        public string tag_id { get; set; }

        /// <summary>   
        ///   可空    性别：男（1）女（2），不填则不做匹配
        /// </summary>  
        public WxSex sex { get; set; }

        /// <summary>   
        ///   可空    客户端版本，当前只具体到系统型号：IOS(1),Android(2),Others(3)，不填则不做匹配
        /// </summary>  
        public WxClientPlatform client_platform_type { get; set; }

        /// <summary>   
        ///   可空    国家信息，是用户在微信中设置的地区，具体请参考地区信息表
        /// </summary>  
        public string country { get; set; }

        /// <summary>   
        ///   可空    省份信息，是用户在微信中设置的地区，具体请参考地区信息表
        /// </summary>  
        public string province { get; set; }

        /// <summary>   
        ///   可空    城市信息，是用户在微信中设置的地区，具体请参考地区信息表
        /// </summary>  
        public string city { get; set; }

        /// <summary>   
        ///   可空    语言信息，是用户在微信中设置的语言，具体请参考语言表：1、简体中文"zh_CN"2、繁体中文TW"zh_TW"3、繁体中文HK"zh_HK"4、英文"en"5、印尼"id"6、马来"ms"7、西班牙"es"8、韩国"ko"9、意大利"it"10、日本"ja"11、波兰"pl"12、葡萄牙"pt"13、俄国"ru"14、泰文"th"15、越南"vi"16、阿拉伯语"ar"17、北印度"hi"18、希伯来"he"19、土耳其"tr"20、德语"de"21、法语"fr"
        /// </summary>  
        public string language { get; set; }
    }



    /// <summary>
    /// 添加定制菜单响应实体
    /// </summary>
    public class WxAddCustomMenuResp:WxBaseResp
    {
        /// <summary>
        /// menuid为菜单id
        /// </summary>
        public long menuid { get; set; }
    }

    /// <summary>
    /// 添加定制菜单响应实体
    /// </summary>
    public class WxUserMenuResp : WxBaseResp
    {
        /// <summary>
        /// 按钮列表
        /// </summary>
        public List<WxMenuButtonMo> button { get; set; }

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
