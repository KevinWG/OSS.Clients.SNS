#region Copyright (C) 2016 OS系列开源项目

/***************************************************************************
*　　	文件功能描述：微信接口SDK基类
*
*　　	创建人： 王超
*       创建人Email：1985088337@qq.com
*    	创建日期：2016
*       
*****************************************************************************/

#endregion

using System;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Common.ComModels;
using OSS.Common.ComModels.Enums;
using OSS.Common.Modules;
using OSS.Common.Modules.LogModule;
using OSS.Http;
using OSS.Http.Mos;

namespace OSS.Social.WX
{
    /// <summary>
    /// 微信接口SDK基类
    /// </summary>
    public abstract class WxBaseApi
    {
        /// <summary>
        ///   默认配置信息，如果实例中的配置为空会使用当前配置信息
        /// </summary>
        public static WxAppCoinfig DefaultConfig { get; set; }

        private readonly WxAppCoinfig _config;

        /// <summary>
        /// 微信接口配置
        /// </summary>
        public WxAppCoinfig ApiConfig
        {
            get
            {
                return _config ?? DefaultConfig;
            }
        }

        /// <summary>
        /// 微信api接口地址
        /// </summary>
        protected const string m_ApiUrl = "https://api.weixin.qq.com";
  
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public WxBaseApi(WxAppCoinfig config)
        {
            if (config == null && DefaultConfig == null)
                throw new ArgumentNullException("config",
                    "构造函数中的config 和 全局DefaultConfig 配置信息同时为空，请通过构造函数赋值，或者在程序入口处给 DefaultConfig 赋值！");
            _config = config;
        }

        /// <summary>
        /// 处理远程请求方法，并返回需要的实体
        /// </summary>
        /// <typeparam name="T">需要返回的实体类型</typeparam>
        /// <param name="request">远程请求组件的request基本信息</param>
        /// <param name="funcFormat">获取实体格式化方法</param>
        /// <returns>实体类型</returns>
        public static async Task<T> RestCommon<T>(OsHttpRequest request,
            Func<HttpResponseMessage, Task<T>> funcFormat = null)
            where T : ResultMo, new()
        {
            T t = default(T);
            try
            {
                var resp = await request.RestSend();
                if (resp.IsSuccessStatusCode)
                {
                    if (funcFormat != null)
                        t = await funcFormat(resp);
                    else
                    {
                        var contentStr = await resp.Content.ReadAsStringAsync();
                        t = JsonConvert.DeserializeObject<T>(contentStr);
                    }
                    if (!t.IsSuccess)
                    {
                        t.Message = GetErrMsg(t.Ret);
                    }
                }
            }
            catch (Exception ex)
            {
                t = new T() {Ret = (int) ResultTypes.InnerError, Message = ex.Message};
                LogUtil.Error(string.Concat("基类请求出错，错误信息：", ex.Message), "RestCommon", ModuleNames.SocialCenter);
            }
            return t ?? new T() {Ret = 0};
        }

        #region   全局错误处理

        /// <summary>
        /// 基本错误信息字典，基类中继续完善
        /// </summary>
        protected static ConcurrentDictionary<int, string> m_DicErrMsg = new ConcurrentDictionary<int, string>();

        static WxBaseApi()
        {
            InitailGlobalErrorCode();
        }


        private static void InitailGlobalErrorCode()
        {
            #region 错误基本信息

            m_DicErrMsg.TryAdd(-1, " 系统繁忙，此时请开发者稍候再试");
            m_DicErrMsg.TryAdd(40001,
                "获取access_token时AppSecret错误，或者access_token无效。请开发者认真比对AppSecret的正确性，或查看是否正在为恰当的公众号调用接口");
            m_DicErrMsg.TryAdd(40002, "不合法的凭证类型");
            m_DicErrMsg.TryAdd(40003, "不合法的OpenID，请开发者确认OpenID（该用户）是否已关注公众号，或是否是其他公众号的OpenID");
            m_DicErrMsg.TryAdd(40004, "不合法的媒体文件类型");
            m_DicErrMsg.TryAdd(40005, "不合法的文件类型");
            m_DicErrMsg.TryAdd(40006, "不合法的文件大小");
            m_DicErrMsg.TryAdd(40007, "不合法的媒体文件id");
            m_DicErrMsg.TryAdd(40008, "不合法的消息类型");
            m_DicErrMsg.TryAdd(40009, "不合法的图片文件大小");
            m_DicErrMsg.TryAdd(40010, "不合法的语音文件大小");
            m_DicErrMsg.TryAdd(40011, "不合法的视频文件大小");
            m_DicErrMsg.TryAdd(40012, "不合法的缩略图文件大小");
            m_DicErrMsg.TryAdd(40013, "不合法的AppID，请开发者检查AppID的正确性，避免异常字符，注意大小写");
            m_DicErrMsg.TryAdd(40014, "不合法的access_token，请开发者认真比对access_token的有效性（如是否过期），或查看是否正在为恰当的公众号调用接口");
            m_DicErrMsg.TryAdd(40015, "不合法的菜单类型");
            m_DicErrMsg.TryAdd(40016, "不合法的按钮个数");
            m_DicErrMsg.TryAdd(40017, "不合法的按钮个数");
            m_DicErrMsg.TryAdd(40018, "不合法的按钮名字长度");
            m_DicErrMsg.TryAdd(40019, "不合法的按钮KEY长度");
            m_DicErrMsg.TryAdd(40020, "不合法的按钮URL长度");
            m_DicErrMsg.TryAdd(40021, "不合法的菜单版本号");
            m_DicErrMsg.TryAdd(40022, "不合法的子菜单级数");
            m_DicErrMsg.TryAdd(40023, "不合法的子菜单按钮个数");
            m_DicErrMsg.TryAdd(40024, "不合法的子菜单按钮类型");
            m_DicErrMsg.TryAdd(40025, "不合法的子菜单按钮名字长度");
            m_DicErrMsg.TryAdd(40026, "不合法的子菜单按钮KEY长度");
            m_DicErrMsg.TryAdd(40027, "不合法的子菜单按钮URL长度");
            m_DicErrMsg.TryAdd(40028, "不合法的自定义菜单使用用户");
            m_DicErrMsg.TryAdd(40029, "不合法的oauth_code");
            m_DicErrMsg.TryAdd(40030, "不合法的refresh_token");
            m_DicErrMsg.TryAdd(40031, "不合法的openid列表");
            m_DicErrMsg.TryAdd(40032, "不合法的openid列表长度");
            m_DicErrMsg.TryAdd(40033, "不合法的请求字符，不能包含\\uxxxx格式的字符");
            m_DicErrMsg.TryAdd(40035, "不合法的参数");
            m_DicErrMsg.TryAdd(40038, "不合法的请求格式");
            m_DicErrMsg.TryAdd(40039, "不合法的URL长度");
            m_DicErrMsg.TryAdd(40050, "不合法的分组id");
            m_DicErrMsg.TryAdd(40051, "分组名字不合法");
            m_DicErrMsg.TryAdd(40117, "分组名字不合法");
            m_DicErrMsg.TryAdd(40118, "media_id大小不合法");
            m_DicErrMsg.TryAdd(40119, "button类型错误");
            m_DicErrMsg.TryAdd(40120, "button类型错误");
            m_DicErrMsg.TryAdd(40121, "不合法的media_id类型");
            m_DicErrMsg.TryAdd(40132, "微信号不合法");
            m_DicErrMsg.TryAdd(40137, "不支持的图片格式");
            m_DicErrMsg.TryAdd(41001, "缺少access_token参数");
            m_DicErrMsg.TryAdd(41002, "缺少appid参数");
            m_DicErrMsg.TryAdd(41003, "缺少refresh_token参数");
            m_DicErrMsg.TryAdd(41004, "缺少secret参数");
            m_DicErrMsg.TryAdd(41005, "缺少多媒体文件数据");
            m_DicErrMsg.TryAdd(41006, "缺少media_id参数");
            m_DicErrMsg.TryAdd(41007, "缺少子菜单数据");
            m_DicErrMsg.TryAdd(41008, "缺少oauth code");
            m_DicErrMsg.TryAdd(41009, "缺少openid");
            m_DicErrMsg.TryAdd(42001, "access_token超时，请检查access_token的有效期，请参考基础支持-获取access_token中，对access_token的详细机制说明");
            m_DicErrMsg.TryAdd(42002, "refresh_token超时");
            m_DicErrMsg.TryAdd(42003, "oauth_code超时");
            m_DicErrMsg.TryAdd(42007, "用户修改微信密码，accesstoken和refreshtoken失效，需要重新授权");
            m_DicErrMsg.TryAdd(43001, "需要GET请求");
            m_DicErrMsg.TryAdd(43002, "需要POST请求");
            m_DicErrMsg.TryAdd(43003, "需要HTTPS请求");
            m_DicErrMsg.TryAdd(43004, "需要接收者关注");
            m_DicErrMsg.TryAdd(43005, "需要好友关系");
            m_DicErrMsg.TryAdd(44001, "多媒体文件为空");
            m_DicErrMsg.TryAdd(44002, "POST的数据包为空");
            m_DicErrMsg.TryAdd(44003, "图文消息内容为空");
            m_DicErrMsg.TryAdd(44004, "文本消息内容为空");
            m_DicErrMsg.TryAdd(45001, "多媒体文件大小超过限制");
            m_DicErrMsg.TryAdd(45002, "消息内容超过限制");
            m_DicErrMsg.TryAdd(45003, "标题字段超过限制");
            m_DicErrMsg.TryAdd(45004, "描述字段超过限制");
            m_DicErrMsg.TryAdd(45005, "链接字段超过限制");
            m_DicErrMsg.TryAdd(45006, "图片链接字段超过限制");
            m_DicErrMsg.TryAdd(45007, "语音播放时间超过限制");
            m_DicErrMsg.TryAdd(45008, "图文消息超过限制");
            m_DicErrMsg.TryAdd(45009, "接口调用超过限制");
            m_DicErrMsg.TryAdd(45010, "创建菜单个数超过限制");
            m_DicErrMsg.TryAdd(45015, "回复时间超过限制");
            m_DicErrMsg.TryAdd(45016, "系统分组，不允许修改");
            m_DicErrMsg.TryAdd(45017, "分组名字过长");
            m_DicErrMsg.TryAdd(45018, "分组数量超过上限");
            m_DicErrMsg.TryAdd(45047, "客服接口下行条数超过上限");
            m_DicErrMsg.TryAdd(46001, "不存在媒体数据");
            m_DicErrMsg.TryAdd(46002, "不存在的菜单版本");
            m_DicErrMsg.TryAdd(46003, "不存在的菜单数据");
            m_DicErrMsg.TryAdd(46004, "不存在的用户");
            m_DicErrMsg.TryAdd(47001, "解析JSON/XML内容错误");
            m_DicErrMsg.TryAdd(48001, "api功能未授权，请确认公众号已获得该接口，可以在公众平台官网-开发者中心页中查看接口权限");
            m_DicErrMsg.TryAdd(48004, "api接口被封禁，请登录mp.weixin.qq.com查看详情");
            m_DicErrMsg.TryAdd(50001, "用户未授权该api");
            m_DicErrMsg.TryAdd(50002, "用户受限，可能是违规后接口被封禁");
            m_DicErrMsg.TryAdd(61451, "参数错误 invalid parameter)");
            m_DicErrMsg.TryAdd(61452, "无效客服账号(invalid kf_account)");
            m_DicErrMsg.TryAdd(61453, "客服帐号已存在(kf_account exsited)");
            m_DicErrMsg.TryAdd(61454, "客服帐号名长度超过限制(仅允许10个英文字符，不包括 @及@后的公众号的微信号)(invalid kf_acount length)");
            m_DicErrMsg.TryAdd(61455, "客服帐号名包含非法字符(仅允许英文+数字)(illegal character in kf_account)");
            m_DicErrMsg.TryAdd(61456, "客服帐号个数超过限制(10个客服账号)(kf_account count exceeded)");
            m_DicErrMsg.TryAdd(61457, "无效头像文件类型(invalid file type)");
            m_DicErrMsg.TryAdd(61450, "系统错误(system error)");
            m_DicErrMsg.TryAdd(61500, "日期格式错误");
            m_DicErrMsg.TryAdd(65301, "不存在此menuid对应的个性化菜单");
            m_DicErrMsg.TryAdd(65302, "没有相应的用户");
            m_DicErrMsg.TryAdd(65303, "没有默认菜单，不能创建个性化菜单");
            m_DicErrMsg.TryAdd(65304, "MatchRule信息为空");
            m_DicErrMsg.TryAdd(65305, "个性化菜单数量受限");
            m_DicErrMsg.TryAdd(65306, "不支持个性化菜单的帐号");
            m_DicErrMsg.TryAdd(65307, "个性化菜单信息为空");
            m_DicErrMsg.TryAdd(65308, "包含没有响应类型的button");
            m_DicErrMsg.TryAdd(65309, "个性化菜单开关处于关闭状态");
            m_DicErrMsg.TryAdd(65310, "填写了省份或城市信息，国家信息不能为空");
            m_DicErrMsg.TryAdd(65311, "填写了城市信息，省份信息不能为空");
            m_DicErrMsg.TryAdd(65312, "不合法的国家信息");
            m_DicErrMsg.TryAdd(65313, "不合法的省份信息");
            m_DicErrMsg.TryAdd(65314, "不合法的城市信息");
            m_DicErrMsg.TryAdd(65316, "该公众号的菜单设置了过多的域名外跳（最多跳转到3个域名的链接）");
            m_DicErrMsg.TryAdd(65317, "不合法的URL");
            m_DicErrMsg.TryAdd(9001001, "POST数据参数不合法");
            m_DicErrMsg.TryAdd(9001002, "远端服务不可用");
            m_DicErrMsg.TryAdd(9001003, "Ticket不合法");
            m_DicErrMsg.TryAdd(9001004, "获取摇周边用户信息失败");
            m_DicErrMsg.TryAdd(9001005, "获取商户信息失败");
            m_DicErrMsg.TryAdd(9001006, "获取OpenID失败");
            m_DicErrMsg.TryAdd(9001007, "上传文件缺失");
            m_DicErrMsg.TryAdd(9001008, "上传素材的文件类型不合法");
            m_DicErrMsg.TryAdd(9001009, "上传素材的文件尺寸不合法");
            m_DicErrMsg.TryAdd(9001010, "上传失败");
            m_DicErrMsg.TryAdd(9001020, "帐号不合法");
            m_DicErrMsg.TryAdd(9001021, "已有设备激活率低于50%，不能新增设备");
            m_DicErrMsg.TryAdd(9001022, "设备申请数不合法，必须为大于0的数字");
            m_DicErrMsg.TryAdd(9001023, "已存在审核中的设备ID申请");
            m_DicErrMsg.TryAdd(9001024, "一次查询设备ID数量不能超过50");
            m_DicErrMsg.TryAdd(9001025, "设备ID不合法");
            m_DicErrMsg.TryAdd(9001026, "页面ID不合法");
            m_DicErrMsg.TryAdd(9001027, "页面参数不合法");
            m_DicErrMsg.TryAdd(9001028, "一次删除页面ID数量不能超过10");
            m_DicErrMsg.TryAdd(9001029, "页面已应用在设备中，请先解除应用关系再删除");
            m_DicErrMsg.TryAdd(9001030, "一次查询页面ID数量不能超过50");
            m_DicErrMsg.TryAdd(9001031, "时间区间不合法");
            m_DicErrMsg.TryAdd(9001032, "保存设备与页面的绑定关系参数错误");
            m_DicErrMsg.TryAdd(9001033, "门店ID不合法");
            m_DicErrMsg.TryAdd(9001034, "设备备注信息过长");
            m_DicErrMsg.TryAdd(9001035, "设备申请参数不合法");
            m_DicErrMsg.TryAdd(9001036, "查询起始值begin不合法");
            m_DicErrMsg.TryAdd(45157, "标签名非法，请注意不能和其他标签重名");
            m_DicErrMsg.TryAdd(45158, "标签名长度超过30个字节");
            m_DicErrMsg.TryAdd(45056, "创建的标签数过多，请注意不能超过100个");
            m_DicErrMsg.TryAdd(45159, "非法的标签");
            m_DicErrMsg.TryAdd(45059, "有粉丝身上的标签数已经超过限制");
            m_DicErrMsg.TryAdd(49003, "传入的openid不属于此AppID");

            #endregion
        }

        /// <summary>
        /// 注册错误码
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        protected static void RegisteErrorCode(int code, string message) => m_DicErrMsg.TryAdd(code, message);

        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <param name="errCode"></param>
        /// <returns></returns>
        protected static string GetErrMsg(int errCode)
            => m_DicErrMsg.ContainsKey(errCode) ? m_DicErrMsg[errCode] : string.Empty;

        #endregion


    }




}
