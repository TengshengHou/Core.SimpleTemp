using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Application.MenuApp;
using Core.SimpleTemp.Application.UserApp;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.SimpleTemp.Common;

namespace Core.SimpleTemp.Application
{
    [AutoDi(typeof(SysLoginService))]
    public interface ISysLoginService
    {
        Task<bool> LoginAsync(HttpContext context, SysUser sysUser);
        Task SignOutAsync(HttpContext context);
        Task<List<SysMenuDto>> GetMenusAndFunctionByUserAsync(SysUserDto sysUserDto);
    }
}
