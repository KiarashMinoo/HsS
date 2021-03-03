using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HsS.Ifs.Chains
{
    public static class ChainServicesExecuter
    {
        private static IServiceScope scope;

        public static void SetServiceProvider(IServiceProvider container)
        {
            scope = container.CreateScope();
        }

        public static Task Execute(object obj, Action<object> reportExecuting = null, params object[] additionalParams)
        {
            var context = new ChainContext(obj, additionalParams);

            foreach (var chainServiceType in ChainContext.ChainServices)
            {
                var parameters = new List<object>();
                var ctor = chainServiceType.GetConstructors().FirstOrDefault();
                if (ctor != null)
                {
                    var ctorParams = ctor.GetParameters();
                    if (ctorParams.Any())
                    {
                        foreach (var ctorParam in ctorParams)
                        {
                            parameters.Add(scope.ServiceProvider.GetService(ctorParam.ParameterType));
                        }
                    }
                }

                var chainService = Activator.CreateInstance(chainServiceType, parameters.ToArray()) as ChainService;
                chainService.Execute(context, reportExecuting);

                if (!chainService.nextAllowed)
                    break;
            }

            return Task.CompletedTask;
        }
    }
}
