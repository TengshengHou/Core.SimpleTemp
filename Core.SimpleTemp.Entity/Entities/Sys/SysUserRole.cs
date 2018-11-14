using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Domain.Entities
{
    public class SysUserRole
    {
        public int SysUserId { get; set; }
        public SysUser SysUser { get; set; }

        public int SysRoleId { get; set; }
        public SysRole SysRole { get; set; }
    }
}
