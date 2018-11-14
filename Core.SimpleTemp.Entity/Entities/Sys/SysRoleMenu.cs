using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Domain.Entities
{
    public class SysRoleMenu
    {

        public int SysRoleId { get; set; }
        public SysRole SysRole { get; set; }

        public int SysMenuId { get; set; }
        public SysMenu SysMenu { get; set; }
    }
}
