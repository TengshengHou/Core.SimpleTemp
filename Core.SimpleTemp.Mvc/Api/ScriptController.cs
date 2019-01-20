using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.SimpleTemp.Application;
using Core.SimpleTemp.Application.ServiceApp.ScriptApp;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.SimpleTemp.Mvc.Api
{
    [Route("api/[controller]")]
    [EnableCors("any")]
    [ApiController]
    public class ScriptController : ControllerBase
    {
        private IScriptAppService  _scriptAppService;
        public ScriptController(IScriptAppService scriptAppService)
        {
            _scriptAppService = scriptAppService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostAsync([FromForm]ScriptDto dto)
        {
           var insertDto=  await _scriptAppService.InsertAsync(dto);
            return Ok(insertDto.Id);
        }
    }
}