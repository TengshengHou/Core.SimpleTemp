using Core.SimpleTemp.Domain.IRepositories;
using Core.SimpleTemp.Mvc.Common;
using Core.SimpleTemp.Repository;
using Core.SimpleTemp.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.SimpleTemp.Mvc
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            //仓储相关
            services.AddDbContext<CoreDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //认证相关
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

            //仓储DI
            services.AddTransient(typeof(ISysUserRepository), typeof(SysUserRepository));

            //Service DI
            services.AddTransient(typeof(ISysLoginService), typeof(SysLoginService));
            services.AddAuthorization();
            services.AddMvc();
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

            //使用静态文件
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

        }
    }
}
