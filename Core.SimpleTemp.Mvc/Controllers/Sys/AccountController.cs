using Core.SimpleTemp.Application;
using Core.SimpleTemp.Application.UserApp;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Mvc.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private ISysLoginService _sysLoginService;
        private IHostingEnvironment _env;


        public AccountController(ISysLoginService sysLoginService, IHostingEnvironment env)
        {
            _sysLoginService = sysLoginService;
            _env = env;
        }

        [HttpGet("Login")]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                bool ret = await _sysLoginService.LoginAsync(HttpContext, new SysUser()
                {
                    LoginName = model.LoginName,
                    Password = model.Password
                });
                if (ret)
                {
                    //登录成功
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                    
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("index", "Home");
                }
            }
            //登录失败
            ModelState.AddModelError("LoginErr", "用户名或密码错误");

            //return RedirectToAction("Login"); //Redirect 302 无法返回服务端验证信息。在无客户的认证情况下很不友好
            return View("Login");
        }


        [HttpGet("SignOut")]
        [Authorize]
        public async Task<IActionResult> SignOutAsync()
        {
            await _sysLoginService.SignOutAsync(HttpContext);
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return new JsonResult(new { msg = "无权限访问" });
        }

        /// <summary>
        /// 初始化admin密码
        /// </summary>
        /// <returns></returns>
        [HttpGet("RestoreAdminPwd")]
        public async Task<IActionResult> RestoreAdminPwdAsync([FromServices] ISysUserAppService sysUserAppService)
        {
            if (_env.IsDevelopment())
            {
                await sysUserAppService.RestoreUserPwdAsync(WebAppConfiguration.AdminLoginName);
            }
            return Ok();
        }

    }
}