using System;
using System.ComponentModel.DataAnnotations;

namespace Core.SimpleTemp.Domain.Entities
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class SysUser : Entity
    {

        [StringLength(30)]
        [Required]
        public string LoginName { get; set; }
        [StringLength(30)]
        [Required]
        public string Password { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime LastUpdate { get; set; }
    }
}
