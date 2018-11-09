using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Core.SimpleTemp.Repository
{
    public class DBInitializer
    {
        private static void Initialize(BaseRepository context)
        {
            context.Database.EnsureCreated();

            ////检查是否需要初始化数据
            //if (context.Students.Any())
            //{
            //    return;   
            //}
            context.SaveChanges();
        }


        public static void Initialize(IWebHost host)
        {

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<BaseRepository>();
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
