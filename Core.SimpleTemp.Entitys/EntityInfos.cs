using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.SimpleTemp.Entitys
{
    //public class EntityInfos : IEntityInfos
    public static class EntityInfos
    {
        const string assemblyNames = "Core.SimpleTemp.Entitys";
        static Dictionary<string, List<PropertyInfo>> _map = new Dictionary<string, List<PropertyInfo>>();
        static EntityInfos()
        {
            if (!String.IsNullOrEmpty(assemblyNames))
            {
                var arrAssemb = assemblyNames.Split(';');
                foreach (var assemblyName in arrAssemb)
                {
                    Assembly assembly = Assembly.Load(assemblyName);
                    List<Type> ts = assembly.GetTypes().ToList();
                    var entityList = ts.Where(t => t.BaseType == typeof(Entity));
                    var v = typeof(SysUser).GetProperties().Where(   p => p.PropertyType.BaseType == typeof(Entity)  || (p.PropertyType.GenericTypeArguments.Length > 0 && p.PropertyType.GenericTypeArguments[0].BaseType == typeof(Entity))).ToList(); 
                    foreach (var entityItem in entityList)
                    {
                        //泛型属性是否是Entity
                        _map.Add(entityItem.FullName, entityItem.GetProperties().Where(
                            p => p.PropertyType.BaseType == typeof(Entity)
                            ||
                            (p.PropertyType.GenericTypeArguments.Length > 0 && p.PropertyType.GenericTypeArguments[0].BaseType == typeof(Entity))
                            ).ToList());
                        //logger.LogInformation("{entityItem.FullName}", entityItem.FullName);
                    }

                }
            }
        }
        public static List<PropertyInfo> GetEntityInfo(string key)
        {
            return _map[key] ?? null;
        }
    }

    //public interface IEntityInfos
    //{
    //    List<PropertyInfo> GetEntityInfo(string key);
    //}
}
