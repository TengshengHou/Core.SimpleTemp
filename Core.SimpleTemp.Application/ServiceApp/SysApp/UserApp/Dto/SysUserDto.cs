﻿using Core.SimpleTemp.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Core.SimpleTemp.Application.UserApp
{
    public class SysUserDto : Dto
    {
        [StringLength(30)]
        [Required]
        public string LoginName { get; set; }
        [StringLength(30)]
        [Required]
        public string Password { get; set; }
        [StringLength(20)]
        [Required]
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime LastUpdate { get; set; }

        public Guid SysDepartmentId { get; set; }
        
        public SysDepartment SysDepartment { get; set; }

        public virtual ICollection<SysUserRoleDto> UserRoles { get; set; }
    }
}
