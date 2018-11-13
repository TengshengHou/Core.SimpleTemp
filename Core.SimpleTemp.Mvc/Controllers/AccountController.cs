using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Service;
using Microsoft.AspNetCore.Mvc;
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
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("index", "Home");
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet("SignOut")]
        public async Task<IActionResult> SignOutAsync()
        {
            await _sysLoginService.SignOutAsync(HttpContext);
            return RedirectToAction("Login");
        }
    }
}