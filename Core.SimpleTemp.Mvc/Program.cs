using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.SimpleTemp.Repository;
using Core.SimpleTemp.Service;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Core.SimpleTemp.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).ConfigureLogging(logging =>
            {
                //logging.ClearProviders();
                logging.AddFile();
            }).Build();

            //初始化DB
            DBInitializer.Initialize(webHost);
            //初始化AutoMapper
            CoreMapper.Initialize();
            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseUrls("http://*:8080")
                .UseStartup<Startup>();
    }
}
