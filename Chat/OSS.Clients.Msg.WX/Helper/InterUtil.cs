using System.Threading.Tasks;

namespace OSS.Clients.Msg.Wechat.Helper
{
   internal static class InterUtil
   {
       public static readonly Task<WechatBaseReplyMsg> NullResult = Task.FromResult<WechatBaseReplyMsg>(null);
   }
}
