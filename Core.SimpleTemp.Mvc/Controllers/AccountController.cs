using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private ISysLoginService _sysLoginService;
        public AccountController(ISysLoginService sysLoginService)
        {
            _sysLoginService = sysLoginService;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(SysUser sysUser, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool ret = await _sysLoginService.LoginAsync(HttpContext, sysUser);
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
        public async Task<IActionResult> SignOutAsync()
        {
            await _sysLoginService.SignOutAsync(HttpContext);
            return RedirectToAction("Login");
        }
    }
}