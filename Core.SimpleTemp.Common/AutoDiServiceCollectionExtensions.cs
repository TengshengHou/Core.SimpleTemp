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
            services.AutoDi("Core.SimpleTemp.Application;Core.SimpleTemp.EntityFrameworkCore", log);
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
                    var list = ts.Where(s => !s.IsInterface);
                    foreach (var item in list)
                    {
                        var autodiAtts = item.GetCustomAttributes<AutoDiAttribute>();
                        if (autodiAtts != null && autodiAtts.Any())
                        {
                            foreach (var attributeItem in autodiAtts)
                            {
                                services.AddTransient(attributeItem._interfaceType, item);
                                if (log != null)
                                    log.LogInformation("services.AddTransient({infterface}, {item})", attributeItem._interfaceType.Name, item.Name);
                            }
                        }
                    }
                }
            }
        }


    }
}
