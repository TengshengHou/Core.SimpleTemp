using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Domain.Entities
{
    public class SysUserRole
    {
        public int UserId { get; set; }
        public SysUser User { get; set; }

        public int RoleId { get; set; }
        public SysRole Role { get; set; }
    }
}
