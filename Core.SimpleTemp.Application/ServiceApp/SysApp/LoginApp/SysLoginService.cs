using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Application.MenuApp;
using Core.SimpleTemp.Application.UserApp;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.SimpleTemp.Common;

namespace Core.SimpleTemp.Application
{
    /// <summary>
    /// 系统登录服务
    /// </summary>
    
    public class SysLoginService : ISysLoginService
    {
        ISysUserRepository _sysUserRepository;
        ISysMenuAppService _sysMenuAppService;
        public SysLoginService(ISysUserRepository sysUserRepository, ISysMenuAppService sysMenuAppService)
        {
            _sysUserRepository = sysUserRepository;
            _sysMenuAppService = sysMenuAppService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public async Task<bool> LoginAsync(HttpContext context, SysUser sysUser)
        {
            var user = await _sysUserRepository.FindUserForLoginAsync(sysUser.LoginName, sysUser.Password);
            if (user == null)
            {
                return false;
            }

            //颁发用户票据
            var claimIdentity = new ClaimsIdentity("Cookie");
            claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Name, user.LoginName));
            var roleIds = await _sysUserRepository.FindUserRoleAsync(user.Id);
            foreach (var roleId in roleIds)
            {
                claimIdentity.AddClaim(new Claim(ClaimTypes.Role, roleId.ToString()));
            }
            var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
            await context.SignInAsync(claimsPrincipal, new AuthenticationProperties()
            {
                IsPersistent = true// Fasle CookieOption.Expires不生效
                //IssuedUtc 颁发时间无需设置默认 CookieScheme的SingInAsync取当前时间，
                //ExpiresUtc 无需设置默认CookieScheme的SingInAsync 当前时间加option.ExpireTimeSpan
                //同时设定CookieOption.Expires为当前 票据过期时间
                //至此建议票据颁发时期均默认使用 option.ExpireTimeSpan设置
            });
            return true;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>

        public Task SignOutAsync(HttpContext context)
        {
            //退出
            context.SignOutAsync();
            //清理内存缓存，待做
            return Task.CompletedTask;
        }


        public Task<List<SysMenuDto>> GetMenusAndFunctionByUserAsync(SysUserDto sysUserDto)
        {
            return _sysMenuAppService.GetMenusAndFunctionByUserAsync(sysUserDto);
        }

        
    }
}
