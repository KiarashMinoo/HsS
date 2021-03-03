using System;
using System.Collections.Generic;
using System.Linq;

namespace HsS.Ifs.Chains
{
    public class ChainContext
    {
        private object contextValue;
        internal static IList<Type> ChainServices;

        static ChainContext()
        {
            var chainServiceType = typeof(ChainService);
            ChainServices = AppDomain.CurrentDomain.GetAssemblies().
                SelectMany(a => a.GetTypes()).
                Where(t => chainServiceType.IsAssignableFrom(t)).
                ToList();
        }

        internal ChainContext(object contextValue, params object[] additionalParams)
        {
            this.contextValue = contextValue;
            AdditionalParameters = new Dictionary<string, object>(additionalParams?.Select(a => new KeyValuePair<string, object>(nameof(a), a)));
        }

        public object ContextValue { get { return contextValue; } }

        public IReadOnlyDictionary<string, object> AdditionalParameters { get; private set; }
    }
}
