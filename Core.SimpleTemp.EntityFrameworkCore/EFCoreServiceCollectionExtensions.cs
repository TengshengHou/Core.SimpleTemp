using Core.SimpleTemp.Domain.IRepositories;
using Core.SimpleTemp.Repository;
using Core.SimpleTemp.Repository.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.EntityFrameworkCore
{
    public static class EFCoreServiceCollectionExtensions
    {
        public static void AddEFCoreRepository(this IServiceCollection services)
        {
            services.AddTransient(typeof(ISysUserRepository), typeof(SysUserRepository));
            services.AddTransient(typeof(ISysMenuRepository), typeof(SysMenuRepository));
            services.AddTransient(typeof(ISysRoleRepository), typeof(SysRoleRepository));
            services.AddTransient(typeof(ISysDepartmentRepository), typeof(SysDepartmentRepository));
            services.AddTransient(typeof(ISysUserRepository), typeof(SysUserRepository));
        }
    }
}
