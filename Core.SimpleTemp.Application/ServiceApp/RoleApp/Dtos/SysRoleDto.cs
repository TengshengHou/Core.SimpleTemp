using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.SimpleTemp.Application.RoleApp
{
    public class SysRoleDto : Dto
    {
        public string Code { get; set; }

        [Required(ErrorMessage = "角色名称不能为空。")]
        public string Name { get; set; }

        public Guid CreateUserId { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Remarks { get; set; }
    }
}
