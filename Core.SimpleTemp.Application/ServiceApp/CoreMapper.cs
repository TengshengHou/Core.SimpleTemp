using AutoMapper;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Application.MenuApp;
using Core.SimpleTemp.Application.RoleApp;
using Core.SimpleTemp.Application.UserApp;
using Core.SimpleTemp.Entitys.Script;
using Core.SimpleTemp.Application.ServiceApp.ScriptApp;
using Core.SimpleTemp.Application.ServiceApp.SysApp.FileApp;
using Core.SimpleTemp.Entitys.Sys;

namespace Core.SimpleTemp.Application
{
    public class CoreMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                #region 内部
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

                cfg.CreateMap<SysFile, SysFileDto>();
                cfg.CreateMap<SysFileDto, SysFile>();
                #endregion

                #region 业务
                cfg.CreateMap<ScriptDetails, ScriptDetailsDto>();
                cfg.CreateMap<ScriptDetailsDto, ScriptDetails>();
                cfg.CreateMap<Script, ScriptDto>();
                cfg.CreateMap<ScriptDto, Script>();
                
                #endregion

            });
        }
    }
}
