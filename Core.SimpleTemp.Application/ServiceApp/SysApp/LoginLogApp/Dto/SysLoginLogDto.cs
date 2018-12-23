using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.SimpleTemp.Application.ServiceApp.SysApp.LoginLogApp
{
    public class SysLoginLogDto : Dto
    {
        [StringLength(30)]
        public string LoginName { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public string Ip { get; set; }
        [StringLength(20)]
        public string LoginType { get; set; }
        public string OtherInfo { get; set; }
        public DateTime? LoginLogTime { get; set; }
    }
}
