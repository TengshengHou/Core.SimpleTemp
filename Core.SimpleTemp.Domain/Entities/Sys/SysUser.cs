using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.SimpleTemp.Domain.Entities
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class SysUser : Entity
    {

        [StringLength(30)]
        public string LoginName { get; set; }
        [StringLength(30)]
        public string Password { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime LastUpdate { get; set; }

        public Guid SysDepartmentId { get; set; }
        public SysDepartment SysDepartment { get; set; }

        public virtual ICollection<SysUserRole> UserRoles { get; set; }
    }
}
