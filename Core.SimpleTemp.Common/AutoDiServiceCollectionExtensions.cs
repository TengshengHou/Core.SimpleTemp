using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.SimpleTemp.Common
{
    public static class AutoDiServiceCollectionExtensions
    {
        public static void AutoDi(this IServiceCollection services, ILogger log = null)
        {
            services.AutoDi("Core.SimpleTemp.Application;Core.SimpleTemp.Repositories", log);
        }


        private static void AutoDi(this IServiceCollection services, string assemblyNames, ILogger log)
        {

            if (!String.IsNullOrEmpty(assemblyNames))
            {
                var arrAssemb = assemblyNames.Split(';');
                foreach (var assemblyName in arrAssemb)
                {
                    Assembly assembly = Assembly.Load(assemblyName);
                    List<Type> ts = assembly.GetTypes().ToList();
                    var interfaceList = ts.Where(s => s.IsInterface);
                    foreach (var interfaceItem in interfaceList)
                    {
                        var autodiAtts = interfaceItem.GetCustomAttributes<AutoDiAttribute>();
                        if (autodiAtts != null && autodiAtts.Any())
                        {
                            foreach (var attributeItem in autodiAtts)
                            {
                                services.AddTransient(interfaceItem, attributeItem.ImplementationType);
                                switch (attributeItem.DiType)
                                {
                                    case AutoDiAttribute.Transient:
                                        services.AddTransient(interfaceItem, attributeItem.ImplementationType);
                                        break;
                                    case AutoDiAttribute.Singleton:
                                        services.AddSingleton(interfaceItem, attributeItem.ImplementationType);
                                        break;
                                    case AutoDiAttribute.Scoped:
                                        services.AddScoped(interfaceItem, attributeItem.ImplementationType);
                                        break;
                                    default:
                                        throw new Exception($"接口{interfaceItem.Name}：指定错误的生命周期");
                                }
                                if (log != null)
                                    log.LogInformation("services.{attributeItem.DiType}({infterface}, {item})", attributeItem.DiType, attributeItem.ImplementationType.Name, interfaceItem.Name);
                            }
                        }
                    }
                }
            }
        }


    }
}
