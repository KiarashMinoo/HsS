using System;
using System.Threading.Tasks;

namespace HsS.Ifs.Chains
{
    public abstract class ChainService
    {
        internal bool nextAllowed = false;

        protected internal abstract Task Execute(ChainContext context, Action<object> executedCallBack = null);

        protected Task Next()
        {
            nextAllowed = true;
            return Task.CompletedTask;
        }
    }
}
