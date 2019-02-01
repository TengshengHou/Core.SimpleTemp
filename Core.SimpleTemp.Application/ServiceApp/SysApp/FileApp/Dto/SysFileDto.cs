using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Application.ServiceApp.SysApp.FileApp
{
    /// <summary>
    /// 文件管理Entity
    /// </summary>
    public class SysFileDto<T> : Dto
    {
        /// <summary>
        /// 业务表ID
        /// </summary>
        public T B_ID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Type { get; set; }// UUID,URL
        public string Extension { get; set; }
        public long Size { get; set; }
        public DateTime UDate { get; set; }
        public DateTime IDate { get; set; }
    }

    /// <summary>
    /// 业务表key为GUID
    /// </summary>
    public class SysFileDto : SysFileDto<Guid>
    {
    }
}
