﻿using Core.SimpleTemp.Application.MenuApp;
using System;

namespace Core.SimpleTemp.Application.RoleApp
{
    public class SysRoleMenuDto
    {
        public Guid SysRoleId { get; set; }
        public SysRoleDto SysRole { get; set; }
        public Guid SysMenuId { get; set; }
        public SysRoleMenuDto SysMenu { get; set; }
    }
}
