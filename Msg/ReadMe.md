��ʹֻ��һ��SDK��ҲҪд��ˮƽ��Ŀ�����㣺1. �����š� 2. ����Ч 

һ. ���ù���

	1. ���� WXChatConfigProvider.DefaultConfig 
	2. ʵ�� IMetaProvider<WXChatConfig> �ӿڣ���ͨ�����캯��ע��

	ʾ�������OSS.Clients.Samples��Ŀ�µ�WXChatController.cs

��. ���ʹ��

	���ȣ�ϵͳԪ�ؽ��ܣ�����ֱ������ʹ��ģʽ���ٻع�ͷ��������

1. ʵ�����Ҳ������Ϣ�������Ҫ��Ϊ:
	a. ������Ϣ���̳��� WXBaseRecMsg ����ͨ��Ϣ �� �̳��� WXBaseRecEventMsg ���¼���Ϣ
		ϵͳĬ��ʵ���� ������ͨ��Ϣ�������¼���Ϣ�����ں�ߵ�ʹ��ģʽ�н��������չ������������

	b. �ظ���Ϣ���̳��� WXBaseReplyMsg ����Ҫ����Ӧ��΢�Žӿڵ�ʵ�壨��ǰ֧������ + WXNoneReplyMsg��
		����������������򷵻���Ϣ�����⼸�����͡���ǰ���ûظ���Ϣ��

		WXTextReplyMsg-�ظ��ı���Ϣ��WXImageReplyMsg-�ظ�ͼƬ��Ϣ��WXVoiceReplyMsg-�ظ�������Ϣ��
		WXVideoReplyMsg-�ظ���Ƶ��Ϣ��WXMusicReplyMsg-�ظ���Ƶ��Ϣ��WXNewsReplyMsg-�ظ�ͼ����Ϣ
			
		WXNoneReplyMsg ��ʾ����Ҫ���Է���Ӧ��ϵͳ�᷵�ظ�΢�Ŷ�success ��
		ʹ���п���ʹ�� WXNoneReplyMsg.None Ĭ��ֵ��

	c. ��Ϣ�����ģ�WXChatContext ����			
		���� RecMsg �� ReplyMsg �������ԣ�Ҳ�����ϱߵĽ�����Ϣ�ͻظ���Ϣ�����������������п���

2. Handler����Ϣ��������࣬ʵ��������Ϣ������������ں�ִ�е���
		��ǰϵͳ�� WXChatBaseHandler �� WXChatHandler ������ǰ����Ϊ���࣬ʵ�����������ڵĿ��ƺ͵��ȡ�
		������ʵ����ϵͳ������Ϣ���¼����壨������ͨ��Ϣ�¼� �� ���Event��Ϣ�¼���

3. Procesor��WXChatProcessor<TRecMsg>������Ϣ�ľ���ִ����.
	���ֻ���ڸ߼�����ģʽ�²Ż���Ҫ�û��Զ���

	
������ܼ��ֿɹ�ʹ�õ�����ģʽ��

1. ����ģʽ 

	ϵͳ WXChatHandler.cs Ĭ��ʵ�ֳ�����������ͨ��Ϣ�������¼���Ϣ��ֻ��Ҫ��д��overwrite����Ӧ���� Process ��ͷ�ķ������ɡ�
	ÿ�������Ĳ�����Ӧ�Ķ�����ϸ����Ϣ���͡����ı�������Ϣ������

    protected override WXBaseReplyMsg ProcessTextMsg(WXTextRecMsg msg)
    {
         return WXNoneReplyMsg.None;
    }

	��ô������д�İ������·�����

	// �����ı���Ϣ
    ProcessTextMsg(WXTextRecMsg msg)

    // ����ͼ����Ϣ
    ProcessImageMsg(WXImageRecMsg msg)

    // ����������Ϣ
    ProcessVoiceMsg(WXVoiceRecMsg msg)

    // ������Ƶ/С��Ƶ��Ϣ
    ProcessVideoMsg(WXVideoRecMsg msg)

    // �������λ����Ϣ
    ProcessLocationMsg(WXLocationRecMsg msg)

    // ����������Ϣ
    ProcessLinkMsg(WXLinkRecMsg msg)

    // �����ע/ȡ����ע�¼�
    ProcessSubscribeEventMsg(WXSubscribeRecEventMsg msg)

    // ����ɨ���������ά���¼�
    ProcessScanEventMsg(WXSubscribeRecEventMsg msg)

    // �����ϱ�����λ���¼�
    ProcessLocationEventMsg(WXLocationRecEventMsg msg)

    // �������˵���ȡ��Ϣʱ���¼�����
    ProcessClickEventMsg(WXClickRecEventMsg msg)

    // �������˵���ת����ʱ���¼�����
    ProcessViewEventMsg(WXViewRecEventMsg msg)

2.  �߼�ģʽ
		�Զ���Processor������ģʽ�ͽ���ģʽ�ж����ڲ�ʵ����Processor�ĵ��ȣ�������Ȼʹ���ϱ�ʾ����
	
	a. ����ʵ�壨�������ʹ�� WXTestRecMsg��

	b. ����CustomeHandler, ��д��ȡProcessor����
	public class CustomeHandler
	{
      protected override WXChatProcessor GetCustomProcessor(
		string msgType,string eventName,IDictionary<string, string> msgInfo)
      {
        if (msgType=="test_msg")
        {
            return new WXChatProcessor<WXTestRecMsg>()
            {
				// ��ί�����������������Ҫ���ͬѧ
				// ���������ͨ�����͵� new t() ����
                RecInsCreater=() => new WXTestRecMsg(),

                // �����¼�����ί��
				// Ҳ����ʹ�������� ProcessTestMsg
				ProcessFunc = msg => WXNoneReplyMsg.None
            };
        }
        return null;
      }
	}
	��ϲ����������˸߼�ģʽ�µĶ��ơ�

��. ����������չ

	�ϱ߽����˼���ģʽ��Ҫʵ�ַ�ʽ����ô��ʵ�ʵ�ʹ�ù��������������Ϣ���ظ��жϣ����ض���Ϣ��ת���ȡ�
	��ϵͳ����Ĳ�ͬ�׶Σ��Ҷ����˼�����Ҫ�Ĵ����¼������������Ϣ����ʱ��ȫ�ֺ;ֲ�����,�ֱ��Ӧ��WXChatBaseHandler����Execute��ͷ���鷽����

	1. Executing(WXChatContext context)����ʼִ���¼�����Ϊ��ΧΪȫ����Ϣ���͡�
		���е���Ϣ���Ͷ��ᾭ������¼���Ȼ��ִ�о�����Ϣ���Ͷ�Ӧ��ί��
		��ʱ msgContext �е� ReplyMsg�������context�е� ReplyMsg ���Ը�ֵ���� ��߶���Ķ�Ӧ�ľ�����Ϣί�з���ִ�С�

		������¼������ǿ��Թ����ظ���Ϣ���û���Ȩ��֤��

	2. ExecuteUnknowProcessor(WXBaseRecMsg msg), δ֪��Ϣ�����¼������÷�ΧΪ����δ���ֶ�Ӧ����ί�е���Ϣ���͡�
		��ִ�о�����¼�ʱ�������ǰ��Ϣ����δ���ҵ���Ӧ�Ĵ���ί�У���ỽ���������
		��Ҫע����ǣ���ʹ��ʹ�õ��� WXChatHandler �����û����д�� ʵ�֣����߷�����Ϊ�գ� Ҳ�ᴥ���˷���

		����ͨ��������������δ֪������Ϣ��־��

	3. ExecuteEnd(WXChatContext msgContext), ִ�н���ʱ���õķ�������Ϊ��ΧΪȫ����Ϣ���͡�
		������Ϣ����ί��ִ�н������ظ�΢����Ӧ֮ǰ��
		��ʱ msgContext �е� ReplyMsg ��Ϊ�գ����ǰ��ִ�з����з���null����ִ�д˷���֮ǰ����Ĭ�ϸ�ֵ WXNoneReplyMsg.None
		�������������ȫ����־��None���͵���Ϣת���ͷ��ȡ�


��. �������� 

	1. ��ģʽ�����ó���������

	����ģʽ����ģʽ�Ѿ���ϵͳ�ڲ�ʵ�֣�ֻ��Ҫ��дί�з������ɣ��򵥷��㣬�������ô󲿷ֵĳ���

	����ģʽ��ֻ��Ҫ��Ϣ���ͣ�����Ϣ����ί�� �ڳ�����ڴ�ע�ἴ�ɣ�����
	�����ó�������������һ������ж������󣬵���Ϣ���MsgType����Ϊ�գ�΢��ĳЩ�����¼��޷�����
	
	�߼�ģʽ��ʹ�õ�������̳�ģʽ��ÿ�����඼����ʵ��ͬһ��Ϣ�����²�ͬ����ί��
	�����õĳ����� ���⻧ƽ̨�����ÿ����Ϣ���ͣ���ͬ��ƽ̨�ȼ����в�ͬ�����ⶨ��ʵ��
		�Լ������������Ϣ�¼�

	����ģʽ�����ȼ�������ģʽ��ʹ��WXChatHandlerʱ�� => �߼�ģʽ => ����ģʽ
	�����������ͬʱ�ڸ߼�ģʽ�ͽ���ģʽ�¶�����һ����Ϣ����Ϊ"test"�Ĵ���ʵ�֣�ϵͳĬ��ʹ�ø߼�ģʽ�µ�ʵ�֡�
	�����Ŀ�����ֱ�Ӽ̳��� WXChatBaseHandler �� ����������ģʽ

	2. �������Եĸ�ֵ����

	������Զ����˽�����Ϣʵ��Ķ�����Ҫ��дFormatPropertiesFromMsg��������� ����ģʽ�� 2.a ��ʵ�֡�
	��Ӧ��Ӧ����Ϣ������Ҫ��ִ��ί����� ToUserName��FromUserName��CreateTime ��ֵ��ϵͳ�Զ�����

	4.  ʹ�÷���ĵط�

	Ϊ�˾����ܼ���ϵͳ�ײ������������Ӱ�죬������ϵͳ�л���û��ʹ�÷�������л����������ط���Ҫע��һ�£�
		1. ��Ҫ�� FormatPropertiesFromMsg �и��Լ������Ը�ֵ��ϵͳ�����ܵ��ṩ��this�������򻯸�ֵ�ķ�ʽ��
		2. �Զ���Processor���̳�WXChatProcessor<TRecMsg>�������ࣩʱ��RecInsCreater�����������ֵ��
		��ϵͳ�ײ��� ������Ӧ��ʵ��ʱ��ͨ�����͵� new() ����ʵ�֣����ڷ��䡣
		
��. �ռ�����
	ǰ����������������еĶ��������ˣ������������...�㻹��Ҫ����Ķ������ɶȣ���ô������Ҳ�����������㡣
	������������չ����ʵ����һ������������������ܵ�ִ�з��������������������¼�Ҳ���������ﴥ����
	
	Resp<WXChatContext> Execute(string recMsgXml)

	�����ϣ���Լ�����һ���������������ڣ�OK����д���Ｔ�ɡ�ϵͳ�����������ǩ����Ϣ����ֵ�����ܵȱ�Ե����
	ֻ��Ҫ��סһ�����飬�������д����������ļ���ģʽ���������������¼�����Ч��
