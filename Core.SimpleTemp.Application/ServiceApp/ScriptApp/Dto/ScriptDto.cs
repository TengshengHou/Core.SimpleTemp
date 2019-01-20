using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Application.ServiceApp.ScriptApp
{
    public class ScriptDto : Dto

    {
        public string S_Name { get; set; }
        public string S_Desc { get; set; }
        public string S_Lable { get; set; }
        public Guid S_IUSER { get; set; }
    }
}
