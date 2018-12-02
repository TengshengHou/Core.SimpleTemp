using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.SimpleTemp.Entitys
{
    public class SysRoleMenu : Entity
    {
        [NotMapped]
        public override Guid Id { get; set; }
        public Guid SysRoleId { get; set; }
        public SysRole SysRole { get; set; }

        public Guid SysMenuId { get; set; }
        public SysMenu SysMenu { get; set; }
    }
}
