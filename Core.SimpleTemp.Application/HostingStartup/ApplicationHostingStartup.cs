using Core.SimpleTemp.Application.MenuApp;
using Core.SimpleTemp.Application.RoleApp;
using Core.SimpleTemp.Application.UserApp;
using Core.SimpleTemp.Domain.IRepositories;
using Core.SimpleTemp.Repository;
using Core.SimpleTemp.Repository.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
[assembly: HostingStartup(typeof(Core.SimpleTemp.Application.HostingStartup.ApplicationHostingStartup))]
namespace Core.SimpleTemp.Application.HostingStartup
{

    public class ApplicationHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureLogging(logging =>
             {
                 //logging.ClearProviders();
                 logging.AddFile();
             });
            builder.UseUrls("http://*:8080");

            builder.ConfigureServices(services => { ConfigureServices(services); });
            builder.Configure(app => { Configure(app); });
        }


        public void ConfigureServices(IServiceCollection services)
        {

            #region 仓储/Service Di
            //仓储DI
            services.AddTransient(typeof(ISysUserRepository), typeof(SysUserRepository));
            services.AddTransient(typeof(ISysMenuRepository), typeof(SysMenuRepository));
            services.AddTransient(typeof(ISysRoleRepository), typeof(SysRoleRepository));
            services.AddTransient(typeof(ISysDepartmentRepository), typeof(SysDepartmentRepository));
            services.AddTransient(typeof(ISysUserRepository), typeof(SysUserRepository));
            //Service DI
            services.AddTransient(typeof(ISysLoginService), typeof(SysLoginService));
            services.AddTransient(typeof(ISysMenuAppService), typeof(SysMenuAppService));
            services.AddTransient(typeof(ISysDepartmentAppService), typeof(SysDepartmentAppService));
            services.AddTransient(typeof(ISysRoleAppService), typeof(SysRoleAppService));
            services.AddTransient(typeof(ISysUserAppService), typeof(SysUserAppService));
            #endregion
        }



        public void Configure(IApplicationBuilder app)
        {
            //初始化AutoMapper
            CoreMapper.Initialize();
        }
    }
}
