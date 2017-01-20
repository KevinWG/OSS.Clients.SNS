#region Copyright (C) 2017 Kevin (OS系列开源项目)

/***************************************************************************
*　　	文件功能描述：公号的功能接口 —— 门店管理实体
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*    	创建日期：2017-1-20
*       
*****************************************************************************/

#endregion

using System.Collections.Generic;
using OS.Social.WX.SysUtils.Mos;

namespace OS.Social.WX.Offcial.Store.Mos
{


    /// <summary>
    /// 添加门店分店响应实体
    /// </summary>
    public class WxAddStoreResp : WxBaseResp
    {
        /// <summary>
        ///    返回的门店分店id
        /// </summary>
        public long poi_id { get; set; }
    }

    /// <summary>
    ///  获取门店信息
    /// </summary>
    public class WxGetStoreResp:WxBaseResp
    {
        /// <summary>
        ///   门店分店信息
        /// </summary>
        public WxStoreMo business { get; set; }
    }

    /// <summary>
    ///  获取门店信息
    /// </summary>
    public class WxGetStoreListResp : WxBaseResp
    {
        /// <summary>
        ///   门店分店信息
        /// </summary>
        public List<WxStoreMo> business_list { get; set; }

        /// <summary>
        ///  总数量
        /// </summary>
        public int total_count { get; set; }
    }

    /// <summary>
    /// 获取门店类目表
    /// </summary>
    public class WxStoreCategoryResp:WxBaseResp
    {
        /// <summary>
        ///  门店类目列表
        /// </summary>
        public List<string> category_list { get; set; }
    }


    /// <summary>
    ///  门店分店信息
    /// </summary>
    public class WxStoreMo
    {
        /// <summary>
        ///  门店分店基础信息
        /// </summary>
        public WxStoreBasicMo base_info { get; set; }
    }


    /// <summary>
    /// 修改门店分店基础服务信息实体
    /// </summary>
    public class WxUpdateStoreBasicServiceReq:WxStoreBasicServiceMo
    {
        /// <summary>
        ///   微信对应的唯一门店id
        /// </summary>
        public long poi_id { get; set; }
    }

    /// <summary>
    ///  门店分店基础信息实体
    /// </summary>
    public class WxStoreBasicMo : WxStoreBasicSmallMo
    {
        /// <summary>
        ///   微信对应的唯一门店id
        /// </summary>
        public long poi_id { get; set; }

        /// <summary>   
        ///   门店是否可用状态。1表示系统错误、2表示审核中、3审核通过、4审核驳回。当该字段为1、2、4状态时，poi_id为空
        /// </summary>  
        public StoreAvailableStatus available_state { get; set; }

        /// <summary>   
        ///   扩展字段是否正在更新中。1表示扩展字段正在更新中，尚未生效，不允许再次更新；0表示扩展字段没有在更新中或更新已生效，可以再次更新
        /// </summary>  
        public StoreUpdateStatus update_status { get; set; }
    }

    /// <summary>
    ///  门店分店基础信息小实体
    /// </summary>
    public class WxStoreBasicSmallMo : WxStoreBasicServiceMo
    {
        /// <summary>   
        ///    必填     门店名称（仅为商户名，如：国美、麦当劳，不应包含地区、地址、分店名等信息，错误示例：北京国美）不能为空，15个汉字或30个英文字符内 
        /// </summary>  
        public string business_name { get; set; }

        /// <summary>   
        ///  必填     分店名称（不应包含地区信息，不应与门店名有重复，错误示例：北京王府井店）20个字以内  
        /// </summary>  
        public string branch_name { get; set; }

        /// <summary>   
        ///  必填     门店所在的省份（直辖市填城市名,如：北京市）10个字以内
        /// </summary>  
        public string province { get; set; }

        /// <summary>   
        ///  必填     门店所在的城市10个字以内
        /// </summary>  
        public string city { get; set; }

        /// <summary>   
        ///   门店所在地区10个字以内必填    
        /// </summary>  
        public string district { get; set; }

        /// <summary>   
        ///  必填     门店所在的详细街道地址（不要填写省市信息）（东莞等没有“区”行政区划的城市，该字段可不必填写。其余城市必填。）
        /// </summary>  
        public string address { get; set; }

        /// <summary>   
        ///   必填  门店的电话（纯数字，区号、分机号均由“-”隔开）  
        /// </summary>  
        public string telephone { get; set; }

        /// <summary>   
        ///   必填   门店的类型（不同级分类用“,”隔开，如：美食，川菜，火锅。详细分类参见附件：微信门店类目表） 
        /// </summary>  
        public List<string> categories { get; set; }

        /// <summary>   
        ///   必填   坐标类型：1为火星坐标  2为sogou经纬度  3为百度经纬度   4为mapbar经纬度   5为GPS坐标   6为sogou墨卡托坐标 
        /// </summary>  
        public int offset_type { get; set; }

        /// <summary>   
        ///   必填    门店所在地理位置的经度
        /// </summary>  
        public float longitude { get; set; }

        /// <summary>   
        ///  必填   门店所在地理位置的纬度（经纬度均为火星坐标，最好选用腾讯地图标记的坐标）  
        /// </summary>  
        public float latitude { get; set; }
    }





    /// <summary>
    /// 门店分店 基础-服务信息
    /// </summary>
    public class WxStoreBasicServiceMo
    {
        /// <summary>   
        ///   可空    商户自己的id，用于后续审核通过收到poi_id的通知时，做对应关系。请商户自己保证唯一识别性  
        /// </summary>  
        public string sid { get; set; }

        /// <summary>   
        ///  可空     图片列表，url形式，可以有多张图片，尺寸为640*340px。必须为上一接口生成的url。图片内容不允许与门店不相关，不允许为二维码、员工合照（或模特肖像）、营业执照、无门店正门的街景、地图截图、公交地铁站牌、菜单截图等
        /// </summary>  
        public List<string> photo_list { get; set; }

        /// <summary>   
        ///  可空  推荐品，餐厅可为推荐菜；酒店为推荐套房；景点为推荐游玩景点等，针对自己行业的推荐内容200字以内   
        /// </summary>  
        public string recommend { get; set; }

        /// <summary>   
        ///  可空     特色服务，如免费wifi，免费停车，送货上门等商户能提供的特色功能或服务
        /// </summary>  
        public string special { get; set; }

        /// <summary>   
        ///  可空   商户简介，主要介绍商户信息等300字以内  
        /// </summary>  
        public string introduction { get; set; }

        /// <summary>   
        ///  可空     营业时间，24小时制表示，用“-”连接，如8:00-20:00
        /// </summary>  
        public string open_time { get; set; }

        /// <summary>   
        ///  可空     人均价格，大于0的整数
        /// </summary>  
        public int avg_price { get; set; }
    }
}
