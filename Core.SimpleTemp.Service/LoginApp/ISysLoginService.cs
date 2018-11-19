using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Service.MenuApp;
using Core.SimpleTemp.Service.UserApp.Dto;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Service
{
    public interface ISysLoginService
    {
        Task<bool> LoginAsync(HttpContext context, SysUser sysUser);
        Task SignOutAsync(HttpContext context);
        Task<List<SysMenuDto>> GetMenusAndFunctionByUserAsync(SysUserDto sysUserDto);
    }
}
