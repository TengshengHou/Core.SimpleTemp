using Core.SimpleTemp.Application.MenuApp;
using Core.SimpleTemp.Application.RoleApp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Application.ServiceApp.DemoApp.RoleMenu
{
    public class RoleMenuDto : Dto
    {

        public override Guid Id { get; set; }
        public Guid SysRoleId { get; set; }
        public SysRoleDto SysRole { get; set; }

        public Guid SysMenuId { get; set; }
        public SysMenuDto SysMenu { get; set; }
    }
}
