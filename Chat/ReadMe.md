即使只是一个SDK，也要写出水平，目标两点：1. 简单优雅。 2. 灵活高效 

一. 配置管理
	1. 通过构造函数传入，适合单一的应用
	2. 通过 SetContext方式 注入，适合多租户，平台的方式使用
	示例详见：OSS.Clients.Samples项目下的WXChatController.cs

二. 框架使用

	首先，系统元素介绍（可以直接跳到使用模式，再回过头来看）：

	1. 实体对象，也就是消息体对象，主要分为:
		a. 接收消息（继承自 WXBaseRecMsg 的普通消息 和 继承自 WXBaseRecEventMsg 的事件消息
		    系统默认实现了 六种普通消息和五种事件消息对象，在后边的使用模式中介绍如何扩展其他对象类型

		b. 回复消息（继承自 WXBaseReplyMsg ，主要是响应给微信接口的实体（当前支持六种 + WXNoneReplyMsg）
			除非特殊情况，否则返回消息就是这几种类型。当前可用回复消息：

			WXTextReplyMsg-回复文本消息，WXImageReplyMsg-回复图片消息，WXVoiceReplyMsg-回复语音消息，
			WXVideoReplyMsg-回复视频消息，WXMusicReplyMsg-回复音频消息，WXNewsReplyMsg-回复图文消息
			
			WXNoneReplyMsg 表示不需要给对方响应，系统会返回给微信端success 。
			使用中可以使用 WXNoneReplyMsg.None 默认值。

		c. 消息上下文（WXChatContext 对象			
			含有 RecMsg 和 ReplyMsg 两个属性，也就是上边的接收消息和回复消息，方便在生命周期中控制

	2. Handler，消息处理控制类，实现整个消息处理的生命周期和执行调度
		当前系统有 WXChatBaseHandler 和 WXChatHandler 两个，前者作为基类，实现了生命周期的控制和调度。
		后者则实现了系统基础消息的事件定义（六个普通消息事件 和 五个Event消息事件）

	3. Procesor（WXChatProcessor<TRecMsg>），消息的具体执行者.
		这个只有在高级定制模式下才会需要用户自定义

	
	下面介绍几种可供使用的三种模式：

	1. 基础模式 
	系统 WXChatHandler.cs 默认实现常见的六种普通消息和五种事件消息，只需要重写（overwrite）对应的以 Process 开头的方法即可。
	每个方法的参数对应的都是详细的消息类型。以文本类型消息举例：

    protected override WXBaseReplyMsg ProcessTextMsg(WXTextRecMsg msg)
    {
         return WXNoneReplyMsg.None;
    }

	那么可以重写的包含以下方法：

	// 处理文本消息
    ProcessTextMsg(WXTextRecMsg msg)

    // 处理图像消息
    ProcessImageMsg(WXImageRecMsg msg)

    // 处理语音消息
    ProcessVoiceMsg(WXVoiceRecMsg msg)

    // 处理视频/小视频消息
    ProcessVideoMsg(WXVideoRecMsg msg)

    // 处理地理位置消息
    ProcessLocationMsg(WXLocationRecMsg msg)

    // 处理链接消息
    ProcessLinkMsg(WXLinkRecMsg msg)

    // 处理关注/取消关注事件
    ProcessSubscribeEventMsg(WXSubscribeRecEventMsg msg)

    // 处理扫描带参数二维码事件
    ProcessScanEventMsg(WXSubscribeRecEventMsg msg)

    // 处理上报地理位置事件
    ProcessLocationEventMsg(WXLocationRecEventMsg msg)

    // 处理点击菜单拉取消息时的事件推送
    ProcessClickEventMsg(WXClickRecEventMsg msg)

    // 处理点击菜单跳转链接时的事件推送
    ProcessViewEventMsg(WXViewRecEventMsg msg)


	2. 进阶模式
		对于不在基础实现类型的消息，系统提供注入消息处理委托的模式来处理消息，以一个 test_msg 消息类型注入示例
	
	a. 定义消息实体：
	public class WXTestRecMsg : WXBaseRecMsg
    {
        public string Test { get; set; }
		// 重写实体实体内部属性赋值
        protected override void FormatPropertiesFromMsg()
        {
            Test = this["Test"];
        }
    }

	b. 定义处理委托：
    static WXTextReplyMsg ProcessTestMsg(WXTestRecMsg msg)
    {
       return new WXTextReplyMsg() { Content = " test_msg 类型消息返回 " };
    }

	c. 注入（内含：RegisteEventProcessor方法）：
	WXChatProcessorProvider.RegisteProcessor<WXTextRecMsg>("test_msg", ProcessTestMsg);

	恭喜，你已经完成了新的消息类型处理注入。

	3.  高级模式
		自定义Processor，基础模式和进阶模式中都在内部实现了Processor的调度，这里依然使用上边示例：
	
	a. 定义实体（这里继续使用 WXTestRecMsg）

	b. 定义CustomeHandler, 重写获取Processor方法
	public class CustomeHandler
	{
      protected override WXChatProcessor GetCustomProcessor(
		string msgType,string eventName,IDictionary<string, string> msgInfo)
      {
        if (msgType=="test_msg")
        {
            return new WXChatProcessor<WXTestRecMsg>()
            {
				// 此委托属性满足对性能有要求的同学
				// 如果不填则通过泛型的 new t() 创建
                RecInsCreater=() => new WXTestRecMsg(),

                // 具体事件处理委托
				// 也可以使用上例的 ProcessTestMsg
				ProcessFunc = msg => WXNoneReplyMsg.None
            };
        }
        return null;
      }
	}
	恭喜，你又完成了高级模式下的定制。


三. 生命周期扩展

	上边讲解了几种模式主要实现方式，那么在实际的使用过程中你会遇到消息的重复判断，对特定消息的转发等。
	在系统处理的不同阶段，我定义了几个主要的处理事件，来满足对消息处理时的全局和局部控制,分别对应在WXChatBaseHandler的以Execute开头的虚方法：

	1. Executing(WXChatContext context)，开始执行事件，作为范围为全部消息类型。
		所有的消息类型都会经过这个事件，然后执行具体消息类型对应的委托
		此时 msgContext 中的 ReplyMsg，如果给context中的 ReplyMsg 属性赋值，则 后边定义的对应的具体消息委托放弃执行。

		在这个事件中我们可以过滤重复消息，用户授权验证等

	2. ExecuteUnknowProcessor(WXBaseRecMsg msg), 未知消息类型事件，作用范围为所有未发现对应处理委托的消息类型。
		在执行具体的事件时，如果当前消息类型未能找到对应的处理委托，则会唤起这个方法
		需要注意的是，即使你使用的是 WXChatHandler ，如果没有重写其 实现，或者返回了为空， 也会触发此方法

		可以通过这个方法中添加未知类型消息日志等

	3. ExecuteEnd(WXChatContext msgContext), 执行结束时调用的方法，作为范围为全部消息类型。
		具体消息处理委托执行结束，回复微信响应之前。
		此时 msgContext 中的 ReplyMsg 不为空，如果前面执行方法中返回null，在执行此方法之前，会默认赋值 WXNoneReplyMsg.None
		可以在这里添加全局日志，None类型的消息转发客服等。


四. 常见问题 

	1. 各模式的适用场景及区别

	基础模式，此模式已经由系统内部实现，只需要重写委托方法即可，简单方便，基本适用大部分的场景

	进阶模式，只需要消息类型，和消息处理委托 在程序入口处注册即可，简单灵活。
	其适用场景：基本满足一般的所有定制需求，但消息体的MsgType不能为空，微信某些特殊事件无法满足
	
	高级模式，使用的是子类继承模式，每个子类都可以实现同一消息类型下不同定制委托
	其适用的场景： 多租户平台下针对每种消息类型，不同的平台等级都有不同的特殊定制实现
		以及所有特殊的消息事件

	几种模式的优先级：基础模式（使用WXChatHandler时） => 高级模式 => 进阶模式
	举例：如果你同时在高级模式和进阶模式下定义了一个消息类型为"test"的处理实现，系统默认使用高级模式下的实现。
	如果你的控制类直接继承了 WXChatBaseHandler 则 不会进入基础模式

	2. 对象属性的赋值问题

	如果你自定义了接收消息实体的对象，需要重写FormatPropertiesFromMsg方法，详见 进阶模式下 2.a 的实现。
	对应响应的消息，不需要在执行委托里给 ToUserName，FromUserName，CreateTime 赋值，系统自动处理。

	4.  使用反射的地方

	为了尽可能减少系统底层给带来的性能影响，所以在系统中基本没有使用反射和序列化，有两个地方需要注意一下：
		1. 需要在 FormatPropertiesFromMsg 中给自己的属性赋值，系统尽可能的提供了this索引来简化赋值的方式。
		2. 自定义Processor（继承WXChatProcessor<TRecMsg>或者子类）时，RecInsCreater属性如果不赋值，
		则系统底层在 创建对应的实例时，通过泛型的 new() 机制实现，属于反射。
		
五. 终极大招
	前面基本都能满足所有的定制需求了，但是如果可能...你还想要更大的定制自由度，那么我这里也尽力的满足你。
	在生命周期扩展中其实还有一个方法，这个方法是总的执行方法，其他的生命周期事件也都是在这里触发：
	
	Resp<WXChatContext> Execute(string recMsgXml)

	如果你希望自己定义一套完整的生命周期，OK，重写这里即可。系统将帮你完成验签，消息对象赋值，加密等边缘操作
	只需要记住一件事情，如果你重写了这里，上述的几种模式和其他生命周期事件将无效。
