#region Copyright (C) 2017 Kevin (OSS开源作坊) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 摇一摇红包接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-5
*       
*****************************************************************************/

#endregion

using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Common.ComModels;
using OSS.Http.Mos;
using OSS.SnsSdk.Official.Wx.Shake.Mos;

namespace OSS.SnsSdk.Official.Wx.Shake
{
    /// <summary>
    /// 微信的摇一摇接口
    /// </summary>
    public partial class WxOffShakeApi : WxOffBaseApi
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">配置信息，如果这里不传，需要在程序入口静态 WxBaseApi.DefaultConfig 属性赋值</param>
        public WxOffShakeApi(AppConfig config=null) : base(config)
        {
        }

        static WxOffShakeApi()
        {
            #region  摇一摇全局错误

            //RegisteErrorCode(9001001, "POST数据参数不合法");
            //RegisteErrorCode(9001002, "远端服务不可用");
            //RegisteErrorCode(9001003, "Ticket不合法");
            //RegisteErrorCode(9001004, "获取摇周边用户信息失败");
            //RegisteErrorCode(9001005, "获取商户信息失败");
            //RegisteErrorCode(9001006, "获取OpenID失败");
            //RegisteErrorCode(9001007, "上传文件缺失");
            //RegisteErrorCode(9001008, "上传素材的文件类型不合法");
            //RegisteErrorCode(9001009, "上传素材的文件尺寸不合法");
            //RegisteErrorCode(9001010, "上传失败");
            //RegisteErrorCode(9001020, "帐号不合法");
            //RegisteErrorCode(9001022, "设备申请数不合法，必须为大于0的数字");
            //RegisteErrorCode(9001023, "已存在审核中的设备ID申请");
            //RegisteErrorCode(9001024, "一次查询设备ID数量不能超过50");
            //RegisteErrorCode(9001025, "设备ID不合法");
            //RegisteErrorCode(9001026, "页面ID不合法");
            //RegisteErrorCode(9001027, "页面参数不合法");
            //RegisteErrorCode(9001028, "一次删除页面ID数量不能超过10");
            //RegisteErrorCode(9001029, "页面已应用在设备中，请先解除应用关系再删除");
            //RegisteErrorCode(9001030, "一次查询页面ID数量不能超过50");
            //RegisteErrorCode(9001031, "时间区间不合法");
            //RegisteErrorCode(9001032, "保存设备与页面的绑定关系参数错误");
            //RegisteErrorCode(9001033, "门店ID不合法");
            //RegisteErrorCode(9001034, "设备备注信息过长");
            //RegisteErrorCode(9001035, "设备申请参数不合法");
            //RegisteErrorCode(9001036, "查询起始值begin不合法");
            //RegisteErrorCode(9001037, "单个设备绑定页面不能超过30个");
            //RegisteErrorCode(9001038, "设备总数超过了限额");
            //RegisteErrorCode(9001039, "不合法的联系人名字");
            //RegisteErrorCode(9001040, "不合法的联系人电话");
            //RegisteErrorCode(9001041, "不合法的联系人邮箱");
            //RegisteErrorCode(9001042, "不合法的行业id");
            //RegisteErrorCode(9001043, "不合法的资质证明文件url，文件需通过“素材管理”接口上传");
            //RegisteErrorCode(9001044, "缺少资质证明文件");
            //RegisteErrorCode(9001045, "申请理由不能超过500字");
            //RegisteErrorCode(9001046, "公众账号未认证");
            //RegisteErrorCode(9001047, "不合法的设备申请批次id");
            //RegisteErrorCode(9001048, "审核状态为审核中或审核已通过，不能再提交申请请求");
            //RegisteErrorCode(9001049, "获取分组元数据失败");
            //RegisteErrorCode(9001050, "账号下分组数达到上限，最多为100个");
            //RegisteErrorCode(9001051, "分组包含的设备数达到上限，最多为10000个");
            //RegisteErrorCode(9001052, "每次添加到分组的设备数达到上限，每次最多操作1000个设备");
            //RegisteErrorCode(9001053, "每次从分组删除的设备数达到上限，每次最多操作1000个设备");
            //RegisteErrorCode(9001054, "待删除的分组仍存在设备");
            //RegisteErrorCode(9001055, "分组名称过长，上限为100个字符");
            //RegisteErrorCode(9001056, "分组待添加或删除的设备列表中包含有不属于该分组的设备id");
            //RegisteErrorCode(9001057, "分组相关信息操作失败");
            //RegisteErrorCode(9001058, "分组id不存在");
            //RegisteErrorCode(9001059, "模板页面logo_url为空");
            //RegisteErrorCode(9001060, "创建红包活动失败");
            //RegisteErrorCode(9001061, "获得红包活动ID失败");
            //RegisteErrorCode(9001062, "创建模板页面失败");
            //RegisteErrorCode(9001063, "红包提供商户公众号ID和红包发放商户公众号ID不一致");
            //RegisteErrorCode(9001064, "红包权限审核失败");
            //RegisteErrorCode(9001065, "红包权限正在审核");
            //RegisteErrorCode(9001066, "红包权限被取消");
            //RegisteErrorCode(9001067, "没有红包权限");
            //RegisteErrorCode(9001068, "红包活动时间不在红包权限有效时间内");
            //RegisteErrorCode(9001069, "设置红包活动开关失败");
            //RegisteErrorCode(9001070, "获得红包活动信息失败");
            //RegisteErrorCode(9001071, "查询红包ticket失败");
            //RegisteErrorCode(9001072, "红包ticket数量超过限制");
            //RegisteErrorCode(9001073, "sponsor_appid与预下单时的wxappid不一致");
            //RegisteErrorCode(9001074, "获得红包发送ID失败");
            //RegisteErrorCode(9001075, "录入活动的红包总数超过创建活动时预设的total");
            //RegisteErrorCode(9001076, "添加红包发送ID失败");
            //RegisteErrorCode(9001077, "解码红包发送ID失败");
            //RegisteErrorCode(9001078, "获取公众号uin失败");
            //RegisteErrorCode(9001079, "接口调用appid与调用创建活动接口的appid不一致");
            //RegisteErrorCode(9001090, "录入的所有ticket都是无效ticket，可能原因为ticket重复使用，过期或金额不在1 - 1000元之间");
            //RegisteErrorCode(9001091, "活动已过期");

            #endregion
        }
    }


    /// <summary>
    ///   微信摇一摇红包接口
    /// </summary>
    public partial class WxOffShakeApi
    {
        /// <summary>
        /// 创建红包活动
        /// </summary>
        /// <param name="userTemplate">是否使用模板，1：使用，2：不使用,以参数的形式拼装在url后。（模版即交互流程图中的红包加载页，使用模板用户不需要点击可自动打开红包；不使用模版需自行开发HTML5页面，并在页面调用红包jsapi）</param>
        /// <param name="logUrl">使用模板页面的logo_url，不使用模板时可不加。展示在摇一摇界面的消息图标。图片尺寸为120x120</param>
        /// <param name="lotteryReq">活动对应内容</param>
        /// <returns></returns>
        public async Task<WxAddLotteryResp> AddLottery(int userTemplate, string logUrl, WxAddLotteryReq lotteryReq)
        {
            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl,
                    $"/shakearound/lottery/addlotteryinfo?use_template={userTemplate}&logo_url={logUrl}"),
                CustomBody = JsonConvert.SerializeObject(lotteryReq)
            };
            return await RestCommonOffcialAsync<WxAddLotteryResp>(req);
        }

        /// <summary>
        ///   录入红包信息
        /// </summary>
        /// <param name="prizeReq">红包内容</param>
        /// <returns></returns>
        public async Task<WxSetLotteryPrizeResp> SetLotteryPrize(WxSetLotteryPrizeReq prizeReq)
        {
            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Post,
                AddressUrl = string.Concat(m_ApiUrl, "/shakearound/lottery/setprizebucket"),
                CustomBody = JsonConvert.SerializeObject(prizeReq)
            };
            
            return await RestCommonOffcialAsync<WxSetLotteryPrizeResp>(req);
        }

        /// <summary>
        /// 设置红包活动抽奖开关
        /// </summary>
        /// <param name="lotteryId">红包抽奖id，来自addlotteryinfo返回的lottery_id</param>
        /// <param name="onoff">活动抽奖开关，0：关闭，1：开启</param>
        /// <returns></returns>
        public async Task<WxBaseResp> SetLotterySwitch(string lotteryId, int onoff)
        {
            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Get,
                AddressUrl =
                    string.Concat(m_ApiUrl,
                        $"/shakearound/lottery/setlotteryswitch?lottery_id={lotteryId}&onoff={onoff}")
            };
            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }

        /// <summary>
        /// 查询红包信息
        /// </summary>
        /// <param name="lotteryId">红包抽奖id，来自addlotteryinfo返回的lottery_id</param>
        /// <returns></returns>
        public async Task<WxGetLotteryResp> GetLotterySwitch(string lotteryId)
        {
            var req = new OsHttpRequest
            {
                HttpMethod = HttpMethod.Get,
                AddressUrl = string.Concat(m_ApiUrl, "/shakearound/lottery/querylottery?lottery_id=", lotteryId)
            };
            return await RestCommonOffcialAsync<WxGetLotteryResp>(req);
        }
    }


}
