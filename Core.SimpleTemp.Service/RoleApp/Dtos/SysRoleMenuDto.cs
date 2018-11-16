using Core.SimpleTemp.Service.MenuApp;
using System;

namespace Core.SimpleTemp.Service.RoleApp.Dtos
{
    public class SysRoleMenuDto
    {
        public Guid SysRoleId { get; set; }
        public SysRoleDto SysRole { get; set; }
        public Guid SysMenuId { get; set; }
        public SysRoleMenuDto SysMenu { get; set; }
    }
}
