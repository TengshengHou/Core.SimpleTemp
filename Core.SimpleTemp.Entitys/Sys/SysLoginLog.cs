using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.SimpleTemp.Entitys.Sys
{
    public class SysLoginLog : Entity
    {
        [StringLength(30)]
        public string LoginName { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Ip { get; set; }
        public string OtherInfo { get; set; }
        [StringLength(10)]
        public string LoginType { get; set; }
        public DateTime? LoginLogTime { get; set; }
    }
}
