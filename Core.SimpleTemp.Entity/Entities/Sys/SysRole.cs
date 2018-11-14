using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Domain.Entities
{
    public class SysRole : Entity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int CreateUserId { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Remarks { get; set; }

    }
}
