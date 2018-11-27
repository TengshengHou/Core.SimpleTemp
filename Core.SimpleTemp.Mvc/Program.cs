using Core.SimpleTemp.Application;
using Core.SimpleTemp.Application.HostingStartup;
using Core.SimpleTemp.Repository;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Core.SimpleTemp.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();
            // 初始化DB
            //DBInitializer.Initialize(webHost);
            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseSetting(WebHostDefaults.HostingStartupAssembliesKey, "Core.SimpleTemp.Application");
            //return WebHost.CreateDefaultBuilder(args)
                 ;
        }
    }
}
