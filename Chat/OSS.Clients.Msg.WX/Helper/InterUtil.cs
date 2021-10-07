using System.Threading.Tasks;
using OSS.Clients.Chat.WX.Mos;

namespace OSS.Clients.Chat.WX.Helper
{
   internal static class InterUtil
   {
       public static readonly Task<WXBaseReplyMsg> NullResult = Task.FromResult<WXBaseReplyMsg>(null);
   }
}
