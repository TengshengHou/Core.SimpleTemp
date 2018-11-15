using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Service.LoginApp
{
    public class SysUserDto
    {
        public Guid Id { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

    }
}
