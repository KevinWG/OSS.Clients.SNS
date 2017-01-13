#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：  用户管理的 用户系列实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-1
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace OS.Social.WX.Offcial.Mos
{
    /// <summary>
    ///  获取openid列表的响应实体
    /// </summary>
    public class WxOpenIdsResp : WxBaseResp
    {
        /// <summary>
        /// 这次获取的粉丝数量
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 拉取列表最后一个用户的openid
        /// </summary>
        public string next_openid { get; private set; }

        /// <summary>
        /// openid 列表
        /// </summary>
        public List<string> openid_list { get; private set; }


        private object _data = null;
        /// <summary>
        /// 微信返回的包含openid的对象，解析出openid_list之后就不用了
        /// </summary>
        public object data {
            get { return _data; }
            set
            {
                _data = value;
                openid_list= _data != null ? ((JToken)data)["openid"].Values<string>().ToList() : new List<string>();
            }
        }
    }




    public class WxUserInfo
    {

    }
}
