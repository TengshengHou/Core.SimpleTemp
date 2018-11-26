using Core.SimpleTemp.Service;
using Core.SimpleTemp.Service.MenuApp;
using Core.SimpleTemp.Service.UserApp;
using Core.SimpleTemp.Service.UserApp.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Core.SimpleTemp.Mvc.ViewComponents
{
    [ViewComponent(Name = "Navigation")]
    public class NavigationViewComponent : ViewComponent
    {
        private readonly ISysLoginService _sysLoginService;
        public NavigationViewComponent(ISysLoginService sysLoginService)
        {
            _sysLoginService = sysLoginService;

        }

        public async System.Threading.Tasks.Task<IViewComponentResult> InvokeAsync()
        {
            var nameIdentifierClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            var id = nameIdentifierClaim.Value;
            var userDto = new SysUserDto() { Id = Guid.Parse(id), LoginName = HttpContext.User.Identity.Name };
            var menus = await _sysLoginService.GetMenusAndFunctionByUserAsync(userDto);

            return View(menus);
        }
    }
}
