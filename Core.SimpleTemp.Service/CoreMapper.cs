﻿using AutoMapper;
using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Service.MenuApp;
using Core.SimpleTemp.Service.RoleApp;
using Core.SimpleTemp.Service.UserApp;

namespace Core.SimpleTemp.Service
{
    public class CoreMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<SysMenu, SysMenuDto>();
                cfg.CreateMap<SysMenuDto, SysMenu>();

                cfg.CreateMap<SysUser, SysUserDto>();
                cfg.CreateMap<SysUserDto, SysUser>();

                cfg.CreateMap<SysDepartmentDto, SysDepartment>();
                cfg.CreateMap<SysDepartment, SysDepartmentDto>();

                cfg.CreateMap<SysRoleMenu, SysRoleMenuDto>();
                cfg.CreateMap<SysRoleMenuDto, SysRoleMenu>();

                cfg.CreateMap<SysRoleDto, SysRole>();
                cfg.CreateMap<SysRole, SysRoleDto>();

                cfg.CreateMap<SysUserRole, SysUserRoleDto>();
                cfg.CreateMap<SysUserRoleDto, SysUserRole>();
                
            });
        }
    }
}
