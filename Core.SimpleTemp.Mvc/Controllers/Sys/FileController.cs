using Core.SimpleTemp.Application.ServiceApp.SysApp.FileApp;
using Core.SimpleTemp.Application.UserApp;
using Core.SimpleTemp.Entitys.Sys;
using Core.SimpleTemp.Mvc.Controllers.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Mvc.Controllers.Sys
{
    [Route("File")]
    public class FileController : AjaxController<SysFileDto, SysFile, ISysFileAppService>
    {
        private readonly ISysFileAppService _service;
        public FileController(ISysFileAppService service) : base(service)
        {
            _service = service;
        }

        [HttpGet("Index")]
        public override IActionResult Index()
        {
            return base.Index();
        }

        [HttpPost("UpdateFile")]
        public async Task<IActionResult> UploadFileAsync(UpdateFileDto files)
        {
            var uploadFileDtoList = await _service.UploadFileAsync(files);
            if (uploadFileDtoList != null && uploadFileDtoList.Any())
                return this.JsonSuccess();
            else
                return this.JsonFaild();

        }
    }
}
