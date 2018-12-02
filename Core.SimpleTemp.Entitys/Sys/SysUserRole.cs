using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.SimpleTemp.Entitys
{
    public class SysUserRole:Entity
    {
        [NotMapped]
        public override Guid Id { get; set; }
        public Guid SysUserId { get; set; }
        public SysUser SysUser { get; set; }

        public Guid SysRoleId { get; set; }
        public SysRole SysRole { get; set; }
    }
}
