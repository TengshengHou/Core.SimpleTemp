using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.SimpleTemp.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.SimpleTemp.Mvc.Controllers.Sys
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ISysLoginService _sysLoginService;

        public AccountController(ISysLoginService sysLoginService)
        {
            _sysLoginService = sysLoginService;
        }

        [HttpGet("GetToken")]
        public async Task<IActionResult> AuthenticateAsync(string userName, string pwd)
        {
            var tokenStr = await _sysLoginService.JwtAuthenticate(userName, pwd);
            return Ok(tokenStr);
        }
    }
}