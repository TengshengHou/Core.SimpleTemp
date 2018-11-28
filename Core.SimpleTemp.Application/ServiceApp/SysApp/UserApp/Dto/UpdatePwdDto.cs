using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application.UserApp
{
    public class UpdatePwdDto
    {
        public string LoginName { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "密码不能为空")]
        public string OldPwd { get; set; }
        [StringLength(30)]
        [Required(ErrorMessage = "密码不能为空")]
        [Compare(nameof(NewPwdTow), ErrorMessage = "两次输入密码不统一")]
        public string NewPwdOne { get; set; }
        [StringLength(30)]
        [Required(ErrorMessage = "密码不能为空")]
        public string NewPwdTow { get; set; }
    }
}
