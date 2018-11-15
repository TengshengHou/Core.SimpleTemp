using AutoMapper;
using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Service.LoginApp;
using Core.SimpleTemp.Service.MenuApp;
using System;
using System.Collections.Generic;
using System.Text;

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
            });
        }
    }
}
