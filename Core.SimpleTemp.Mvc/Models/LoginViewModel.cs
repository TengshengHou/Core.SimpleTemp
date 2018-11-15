using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Mvc.Models
{
    public class LoginViewModel
    {
        [StringLength(30)]
        [Required(ErrorMessage = "用户名不能为空")]
        public string LoginName { get; set; }
        [StringLength(30)]
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}
