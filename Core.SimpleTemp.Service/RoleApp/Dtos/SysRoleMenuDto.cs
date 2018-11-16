using Core.SimpleTemp.Service.MenuApp;
using System;

namespace Core.SimpleTemp.Service.RoleApp.Dtos
{
    public class SysRoleMenuDto
    {
        public Guid RoleId { get; set; }
        public SysRoleDto SysRole { get; set; }
        public Guid MenuId { get; set; }
        public SysRoleMenuDto SysMenu { get; set; }
    }
}
