using Core.SimpleTemp.Application.Authorization;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Mvc.Filters;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Linq;
using System.Text;
namespace Core.SimpleTemp.Mvc
{
    public class Startup
    {
        private const string WEBAPP_OPTIONS = "WebAppOptions";
        public IConfiguration _configuration { get; }
        private readonly ILogger _logger;
        private readonly WebAppOptions _webAppOptions;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _webAppOptions = _configuration.GetSection(WEBAPP_OPTIONS).Get<WebAppOptions>();
        }


        public void ConfigureServices(IServiceCollection services)
        {

            #region 数据仓储链接设置 DbContext
            services.AddDbContext<CoreDBContext>(options =>
               {
                   options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
                   //options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
               });

            //多Db链接实例
            services.AddDbContext<LogDBContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("LogConnection"));
                //options.UseNpgsql(_configuration.GetConnectionString("LogConnection"));
            });
            #endregion


            #region 认证相关
            services.AddAuthentication(option =>
          {
              option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              //只设置上面一个DeafultScheme即可，一下均为重复设置。为方便以后更换Scheme 思路清晰
              option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              option.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
              option.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;

          }).AddCookie(option =>
          {

              option.Cookie.SameSite = SameSiteMode.None;
              //设置Cookie过期时间 ,如不设置 默认为14天挺恐怖的 注意如不设置票据过期时间，默认票据采用此时间

              option.ExpireTimeSpan = TimeSpan.FromMinutes(_webAppOptions.TimeOutOfLogin);
              //当Cookie过期时间已达一半时，是否重置ExpireTimeSpan 每次认证确认，handle 在Http响应重写Cookie
              option.SlidingExpiration = true;

              //SessionStore 暂时放弃现功能
              //第一版不支持ValidatePrincipal 以后再说
              //option.Events = new CookieAuthenticationEvents() {
              //    OnValidatePrincipal
              //};
          }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, JwtOption =>
          {
              //禁用Https模式
              JwtOption.RequireHttpsMetadata = false;
              JwtOption.TokenValidationParameters = new TokenValidationParameters
              {
                  ValidateAudience = true,
                  ValidAudience = _webAppOptions.JwtValidAudience,
                  ValidateIssuer = true,
                  ValidIssuer = _webAppOptions.JwtValidIssuer,
                  //是否验证失效时间
                  ValidateLifetime = true,

                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_webAppOptions.JwtIssuerSigningKey))
              };
          });
            #endregion

            services.Configure<WebAppOptions>(_configuration.GetSection(WEBAPP_OPTIONS));

            //AutoDI
            services.AutoDi(_logger);

            //自定义授权
            services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
            //services.AddAuthorization();
            //采用内存版分布缓存 方便以后切换Redis
            services.AddDistributedMemoryCache(); //services.AddDistributeRedisCache(null);
            services.AddHttpClient();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc(options =>
            {
                //自定义全局异常过滤器
                options.Filters.Add<HttpGlobalExceptionFilter>();
            });

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
                //app.UseExceptionHandler("/Shared/Error");
                app.UseExceptionHandler(new ExceptionHandlerOptions()
                {
                    ExceptionHandler = async context =>
                    {
                        //Ajax处理
                        if (context.Request.IsAjaxRequest() || context.Request.Path.Value.StartsWith("/api/"))
                        {
                            context.Response.StatusCode = 200;
                            context.Response.ContentType = "application/json;charset=utf-8";
                            var err = context.Features.Get<IExceptionHandlerFeature>();
                            var errStr = err.Error.Message;
                            var sb = context.Response.CreateAjaxResponseExceptionJson(errStr);
                            await context.Response.WriteAsync(sb.ToString());
                        }
                        else
                        {
                            context.Response.Redirect("/error");
                        }

                    }
                });

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
