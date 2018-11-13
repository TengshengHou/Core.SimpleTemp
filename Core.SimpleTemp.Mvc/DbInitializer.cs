using Core.SimpleTemp.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Core.SimpleTemp.Repository
{
    public class DBInitializer
    {
        private static void Initialize(CoreDBContext context)
        {
            context.Database.EnsureCreated();

            //检查是否需要初始化数据
            if (context.SysUser.Any())
            {
                return;
            }

            //添加系统默认管理员
            context.SysUser.Add(new SysUser { LoginName = "admin", Password = "admin", LastUpdate = DateTime.Now });
            context.SaveChanges();
        }


        public static void Initialize(IWebHost host)
        {

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<CoreDBContext>();
                    DBInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<DBInitializer>>();
                    logger.LogError(ex, "初始化数据库异常.");
                }
            }

        }


    }


}
