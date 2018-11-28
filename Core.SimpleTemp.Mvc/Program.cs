using Core.SimpleTemp.Application;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
namespace Core.SimpleTemp.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();
            // 初始化DB
            DBInitializer.Initialize(webHost);
            CoreMapper.Initialize();
            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddFile();
            }).UseUrls("http://*:8080").UseStartup<Startup>();
        }
    }
}
