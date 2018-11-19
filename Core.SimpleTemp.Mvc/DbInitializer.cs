﻿using Core.SimpleTemp.Domain.Entities;
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

            //增加一个部门
            context.SysDepartment.Add(
             new SysDepartment
             {
                 Name = "总部",
             }
          );
            context.SaveChanges();
            var dep = context.SysDepartment.FirstOrDefault();

            //添加系统默认管理员
            context.SysUser.Add(new SysUser { LoginName = "admin", Password = "admin", LastUpdate = DateTime.Now, SysDepartmentId = dep.Id });
            context.SysUser.Add(new SysUser { LoginName = "admin2", Password = "admin", LastUpdate = DateTime.Now, SysDepartmentId = dep.Id });
            //增加四个基本功能菜单
            context.SysMenu.AddRange(
                  new SysMenu
                  {

                      ParentId = Guid.Empty,
                      Name = "顶级菜单",
                      Code = "Department",
                      SerialNumber = 0,
                      Icon = "fa fa-link"
                  }

            );

            context.SaveChanges();
            var topMenu = context.SysMenu.SingleOrDefault(m => m.ParentId == Guid.Empty);
            context.SysMenu.AddRange(new SysMenu
            {
                Name = "组织机构管理",
                Code = "Department",
                SerialNumber = 0,
                Icon = "fa fa-link",
                Url = "/Department/index",
                ParentId = topMenu.Id
            },
             new SysMenu
             {
                 Name = "角色管理",
                 Code = "Role",
                 SerialNumber = 1,
                 Icon = "fa fa-link",
                 Url = "/role/index",
                 ParentId = topMenu.Id
             },
             new SysMenu
             {
                 Name = "用户管理",
                 Code = "User",
                 SerialNumber = 2,
                 Icon = "fa fa-link",
                 ParentId = topMenu.Id,
                 Url = "/user/index"
             },
             new SysMenu
             {
                 Name = "功能管理",
                 Code = "Menu",
                 SerialNumber = 3,
                 Icon = "fa fa-link",
                 ParentId = topMenu.Id,
                 Url = "/Menu/index"
             });

            context.SysRole.Add(new SysRole()
            {
                Code = "Admin",
                Name = "管理员",
                Remarks = "管理员",
                CreateTime = DateTime.Now
            });
            context.SaveChanges();
            context.SysUserRole.Add(new SysUserRole
            {
                SysRoleId = context.SysRole.FirstOrDefault(r => r.Name == "admin").Id,
                SysUserId = context.SysUser.FirstOrDefault(u => u.LoginName == "admin").Id
            });
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
