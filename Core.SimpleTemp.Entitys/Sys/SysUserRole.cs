using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Entitys
{
    public class SysUserRole
    {
        public Guid SysUserId { get; set; }
        public SysUser SysUser { get; set; }

        public Guid SysRoleId { get; set; }
        public SysRole SysRole { get; set; }
    }
}
