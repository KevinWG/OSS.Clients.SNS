#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 摇一摇红包接口
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-2-18
*       
*****************************************************************************/

#endregion

using System.Threading.Tasks;
using Newtonsoft.Json;
using OSS.Http.Mos;
using OSS.Social.WX.Offcial.Shake.Mos;

namespace OSS.Social.WX.Offcial.Shake
{
    /// <summary>
    /// 微信的摇一摇接口
    /// </summary>
    public partial class WxOffShakeApi : WxOffBaseApi
    {
        public WxOffShakeApi(WxAppCoinfig config) : base(config)
        {
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
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl,
                $"/shakearound/lottery/addlotteryinfo?use_template={userTemplate}&logo_url={logUrl}");
            req.CustomBody = JsonConvert.SerializeObject(lotteryReq);

            return await RestCommonOffcialAsync<WxAddLotteryResp>(req);
        }

        /// <summary>
        ///   录入红包信息
        /// </summary>
        /// <param name="prizeReq">红包内容</param>
        /// <returns></returns>
        public async Task<WxSetLotteryPrizeResp> SetLotteryPrize(WxSetLotteryPrizeReq prizeReq)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.POST;
            req.AddressUrl = string.Concat(m_ApiUrl,"/shakearound/lottery/setprizebucket");
            req.CustomBody = JsonConvert.SerializeObject(prizeReq);

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
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = string.Concat(m_ApiUrl, $"/shakearound/lottery/setlotteryswitch?lottery_id={lotteryId}&onoff={onoff}");

            return await RestCommonOffcialAsync<WxBaseResp>(req);
        }

        /// <summary>
        /// 查询红包信息
        /// </summary>
        /// <param name="lotteryId">红包抽奖id，来自addlotteryinfo返回的lottery_id</param>
        /// <returns></returns>
        public async Task<WxGetLotteryResp> GetLotterySwitch(string lotteryId)
        {
            var req = new OsHttpRequest();

            req.HttpMothed = HttpMothed.GET;
            req.AddressUrl = string.Concat(m_ApiUrl, "/shakearound/lottery/querylottery?lottery_id=",lotteryId);

            return await RestCommonOffcialAsync<WxGetLotteryResp>(req);
        }
    }


}
