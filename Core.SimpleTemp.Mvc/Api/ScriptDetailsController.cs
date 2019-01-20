using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
    public class ScriptDetailsController : ControllerBase
    {
        private IScriptDetailsAppService _scriptDetailsAppService;
        public ScriptDetailsController(IScriptDetailsAppService scriptDetailsAppService)
        {
            _scriptDetailsAppService = scriptDetailsAppService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostAsync([FromForm]ScriptDetailsDto dto)
        {
            var insertDto = await _scriptDetailsAppService.InsertAsync(dto);
            return Ok(insertDto.Id);
        }

        [HttpGet("CreateLua")]
        public async Task<ActionResult<string>>  CreateLuaAsync(Guid guid)
        {
            //click1(991, 674);
            //sleep(2000);

            //inputString(2960000000050);
            //enter();
            StringBuilder sb = new StringBuilder();
            var list = await _scriptDetailsAppService.GetAllListAsync(sd => sd.SD_Script_ID == guid);
            foreach (var item in list)
            {
                if (item.SD_EventType == "click")
                {
                    var xyArry = item.SD_Content.Split('|');
                    if (!Equals(xyArry, null) && xyArry.Any())
                    {
                        var x = xyArry[0];
                        var y = xyArry[1];
                        sb.AppendLine($"click1({x},{y});");
                    }
                }
                else
                {
                    sb.AppendLine($"inputString({item.SD_Content});");
                    sb.AppendLine($"enter()");
                }
            }


            //byte[] array = Encoding.ASCII.GetBytes(sb.ToString());
            //MemoryStream stream = new MemoryStream(array);
            ////
            //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            ////response.Content = new StreamContent(stream);
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            //response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            //{
            //    FileName = "Wep Api Demo File.zip"
            //};
            //response.Content = new ByteArrayContent(array);


            //return response;
            return Ok(sb.ToString());
        }
    }
}