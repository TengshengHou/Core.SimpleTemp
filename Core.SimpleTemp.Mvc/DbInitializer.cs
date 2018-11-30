using Core.SimpleTemp.Common;
using Core.SimpleTemp.Application.Authorization;
using Core.SimpleTemp.Entitys;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Core.SimpleTemp.Repository;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal;

namespace Core.SimpleTemp.Mvc
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
            context.SysUser.Add(new SysUser { LoginName = WebAppConfiguration.AdminLoginName, Password = WebAppConfiguration.InitialPassword, LastUpdate = DateTime.Now, SysDepartmentId = dep.Id });
            context.SysUser.Add(new SysUser { LoginName = "admin2", Password = WebAppConfiguration.InitialPassword, LastUpdate = DateTime.Now, SysDepartmentId = dep.Id });


            #region 增加四个基本功能菜单
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
                Code = "Department_Index",
                SerialNumber = 0,
                Icon = "fa fa-link",
                Url = "/Department/index",
                ParentId = topMenu.Id
            },
             new SysMenu
             {
                 Name = "角色管理",
                 Code = "Role_Index",
                 SerialNumber = 1,
                 Icon = "fa fa-link",
                 Url = "/role/index",
                 ParentId = topMenu.Id
             },
             new SysMenu
             {
                 Name = "用户管理",
                 Code = "UserController_Index",
                 SerialNumber = 2,
                 Icon = "fa fa-link",
                 ParentId = topMenu.Id,
                 Url = "/user/index"
             },
             new SysMenu
             {
                 Name = "功能管理",
                 Code = "Menu_Index",
                 SerialNumber = 3,
                 Icon = "fa fa-link",
                 ParentId = topMenu.Id,
                 Url = "/Menu/index"
             });

            context.SaveChanges();
            #endregion

            #region 用户管理功能初始化
            var parentMenu = context.SysMenu.SingleOrDefault(m => m.Name == "用户管理");
            //var prefix = "UserController_";
            context.SysMenu.AddRange(new SysMenu
            {
                Name = "GetUserByDepartment",
                Code = UserPermission.UserController_GetUserByDepartment,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "Edit",
                Code = UserPermission.UserController_Edit,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "DeleteMuti",
                Code = UserPermission.UserController_DeleteMuti,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "Delete",
                Code = UserPermission.UserController_Delete,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "Get",
                Code = UserPermission.UserController_Get,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            });
            context.SaveChanges();
            #endregion

            #region 组织机构管理功能初始化
            parentMenu = context.SysMenu.SingleOrDefault(m => m.Name == "组织机构管理");
            context.SysMenu.AddRange(new SysMenu
            {
                Name = "Edit",
                Code = DepartmentPermission.Department_Edit,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "DeleteMuti",
                Code = DepartmentPermission.Department_DeleteMuti,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "Delete",
                Code = DepartmentPermission.Department_Delete,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "Get",
                Code = DepartmentPermission.Department_Get,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "GetTreeData",
                Code = DepartmentPermission.Department_GetTreeData,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "GetChildrenByParent",
                Code = DepartmentPermission.Department_GetChildrenByParent,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            });
            context.SaveChanges();
            #endregion

            #region Menu
            parentMenu = context.SysMenu.SingleOrDefault(m => m.Name == "功能管理");
            context.SysMenu.AddRange(new SysMenu
            {
                Name = "Edit",
                Code = MenuPermission.Menu_Edit,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "DeleteMuti",
                Code = MenuPermission.Menu_DeleteMuti,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "Delete",
                Code = MenuPermission.Menu_Delete,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "Get",
                Code = MenuPermission.Menu_Get,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "GetTreeData",
                Code = MenuPermission.Menu_GetMenuTreeData,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "GetMneusByParent",
                Code = MenuPermission.Menu_GetMneusByParent,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            });
            context.SaveChanges();
            #endregion



            #region Role
            parentMenu = context.SysMenu.SingleOrDefault(m => m.Name == "角色管理");
            context.SysMenu.AddRange(new SysMenu
            {
                Name = "Edit",
                Code = RolePermission.Role_Edit,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "DeleteMuti",
                Code = RolePermission.Role_DeleteMuti,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "Delete",
                Code = RolePermission.Role_Delete,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "Get",
                Code = RolePermission.Role_Get,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "GetAllPageList",
                Code = RolePermission.Role_GetAllPageList,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "GetMenusByRole",
                Code = RolePermission.Role_GetMenusByRole,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            }, new SysMenu
            {
                Name = "SavePermission",
                Code = RolePermission.Role_SavePermission,
                SerialNumber = 0,
                Icon = "fa-circle-o",
                Type = 1,
                ParentId = parentMenu.Id
            });
            context.SaveChanges();
            #endregion

            //角色
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
                SysRoleId = context.SysRole.FirstOrDefault(r => r.Name == "管理员").Id,
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
