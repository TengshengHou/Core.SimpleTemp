using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Entitys.Script
{

    public class ScriptDetails : Entity
    {
        public Guid SD_Script_ID { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string SD_Content { get; set; }
        public string SD_EventType { get; set; }
        public string SD_EventContext { get; set; }
        public DateTime SD_Datetime { get; set; }
    }
}
