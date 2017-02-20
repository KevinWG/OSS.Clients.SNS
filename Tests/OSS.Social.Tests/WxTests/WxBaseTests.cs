using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSS.Common;
using OSS.Common.Modules.DirConfigModule;
using OSS.Social.WX;

namespace OSS.Social.Tests.WxTests
{
    /// <summary>
    /// WxBasicTests 的摘要说明
    /// </summary>
    [TestClass]
    public class WxBaseTests
    {
        protected static WxAppCoinfig m_Config = null;

        static WxBaseTests()
        {
            // 可以在这里初始化appid 等配置信息，也可以在下边直接赋值
            // DirConfigUtil.SetDirConfig("my_weixin_appconfig",new TestConfigInfo(){})
            var config = DirConfigUtil.GetDirConfig<TestConfigInfo>("my_weixin_appconfig");
            if (config==null)
            {
                throw new ArgumentException("请将下边的配置信息直接赋值，或者通过DirConfigUtil.SetDirConfig初始化一下基础配置信息");
            }
            m_Config = config.WxConfig;
            
            OsConfig.CacheProvider=moduleName =>
            {
                if (config!=null)
                {
                    return new RedisCache(0, config.RedisConnectionStr);
                }
                return null;//  如果为空会走系统缓存
            };
        }


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
    }


    public class TestConfigInfo
    {
        public WxAppCoinfig WxConfig { get; set; }
        public string RedisConnectionStr { get; set; }
    }
}
