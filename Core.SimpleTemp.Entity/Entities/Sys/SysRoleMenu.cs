using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Domain.Entities
{
    public class SysRoleMenu
    {

        public Guid SysRoleId { get; set; }
        public SysRole SysRole { get; set; }

        public Guid SysMenuId { get; set; }
        public SysMenu SysMenu { get; set; }
    }
}
