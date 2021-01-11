using System;
using System.Threading.Tasks;
// ReSharper disable All 
namespace Cult.Extensions.ExtraTask
{
    public static class TaskExtensions
    {
        public static async void SafeFireAndForget(this Task @this, bool continueOnCapturedContext = true, Action<Exception> onException = null)
        {
            try
            {
                await @this.ConfigureAwait(continueOnCapturedContext);
            }
            catch (Exception e) when (onException != null)
            {
                onException(e);
            }
        }
    }
}
