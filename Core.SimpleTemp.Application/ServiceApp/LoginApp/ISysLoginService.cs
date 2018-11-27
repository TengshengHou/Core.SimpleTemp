using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Application.MenuApp;
using Core.SimpleTemp.Application.UserApp;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application
{
    public interface ISysLoginService
    {
        Task<bool> LoginAsync(HttpContext context, SysUser sysUser);
        Task SignOutAsync(HttpContext context);
        Task<List<SysMenuDto>> GetMenusAndFunctionByUserAsync(SysUserDto sysUserDto);
    }
}
