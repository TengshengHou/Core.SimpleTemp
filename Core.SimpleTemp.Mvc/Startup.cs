using Core.SimpleTemp.Application.Authorization;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace Core.SimpleTemp.Mvc
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            //DbContext
            services.AddDbContext<CoreDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                //options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            #region 认证相关
            services.AddAuthentication(option =>
          {
              option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              //只设置一个DeafultScheme即可，一下均为重复设置。为方便以后更换Scheme 思路清晰
              option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              option.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              option.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;

          }).AddCookie(option =>
          {
              //设置Cookie过期时间 ,如不设置 默认为14天挺恐怖的 注意如不设置票据过期时间，默认票据采用此时间
              option.ExpireTimeSpan = TimeSpan.FromMinutes(WebAppConfiguration.TimeOutOfLogin);

              //当Cookie过期时间已达一半时，是否重置ExpireTimeSpan 每次认证确认，handle 在Http响应重写Cookie
              option.SlidingExpiration = true;

              //SessionStore 待实现功能

              //第一版不支持ValidatePrincipal 以后再说
              //option.Events = new CookieAuthenticationEvents() {
              //    OnValidatePrincipal
              //};
          });
            #endregion

            //AutoDI
            services.AutoDi(_logger);

            //自定义授权处理
            services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
            //services.AddAuthorization();
            //采用内存版分布缓存 方便以后切换Redis
            services.AddDistributedMemoryCache(); //services.AddDistributeRedisCache(null);
            services.AddMvc();
            services.AddHttpClient();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //生产环境异常处理
                app.UseExceptionHandler("/Shared/Error");
            }

            #region 静态文件
            var cachePeriod = env.IsDevelopment() ? "600" : "604800";//七天
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                },
            });
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                },
                FileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory())
            });
            #endregion

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

        }
    }
}
