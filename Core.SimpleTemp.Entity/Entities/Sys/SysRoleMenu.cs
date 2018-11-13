using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Domain.Entities
{
    public class SysRoleMenu
    {

        public int RoleId { get; set; }
        public SysRoleMenu Role { get; set; }

        public int MenuId { get; set; }
        public SysMenu Menu { get; set; }
    }
}
