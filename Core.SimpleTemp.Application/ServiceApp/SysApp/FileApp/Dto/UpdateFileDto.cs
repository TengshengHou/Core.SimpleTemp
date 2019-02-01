using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Application.ServiceApp.SysApp.FileApp
{
    public class UpdateFileDto<T> : Dto
    {
        /// <summary>
        /// 业务表ID
        /// </summary>
        public T B_ID { get; set; }
        public List<IFormFile> files { get; set; }
    }

    public class UpdateFileDto : UpdateFileDto<Guid>
    {

    }
}
