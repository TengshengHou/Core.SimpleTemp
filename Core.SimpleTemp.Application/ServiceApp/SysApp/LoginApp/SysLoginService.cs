﻿using Core.SimpleTemp.Application.MenuApp;
using Core.SimpleTemp.Application.ServiceApp.SysApp.LoginLogApp;
using Core.SimpleTemp.Application.UserApp;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application
{
    /// <summary>
    /// 系统登录服务
    /// </summary>

    public class SysLoginService : ISysLoginService
    {
        ISysUserRepository _sysUserRepository;
        ISysMenuAppService _sysMenuAppService;
        IDistributedCache _distributedCache;
        ISysLoginLogAppService _sysLoginLogAppService;
        readonly WebAppOptions _webAppOptions;
        public SysLoginService(ISysUserRepository sysUserRepository, ISysMenuAppService sysMenuAppService, IDistributedCache distributedCache, ISysLoginLogAppService sysLoginLogAppService, IOptionsMonitor<WebAppOptions> webAppOptions)
        {
            _sysUserRepository = sysUserRepository;
            _sysMenuAppService = sysMenuAppService;
            _distributedCache = distributedCache;
            _sysLoginLogAppService = sysLoginLogAppService;
            _webAppOptions = webAppOptions.CurrentValue;
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
            //登录日志
            await _sysLoginLogAppService.InsertLogAsync(new SysLoginLogDto { LoginName = user.LoginName, Name = user.Name, LoginType = LoginType.Cookie });
            //颁发用户票据
            var claimIdentity = this.CreateClaimsIdentity(user);
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
        /// 登录
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public async Task<string> JwtAuthenticate(string userName, string pwd)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_webAppOptions.JwtIssuerSigningKey);//Jwt秘钥
            var authTime = DateTime.UtcNow;
            var expiresAt = authTime.AddMinutes(_webAppOptions.TimeOutOfLogin);//过期时间

            //验证账号&密码信息
            var user = await _sysUserRepository.FindUserForLoginAsync(userName, pwd);
            if (user == null)
            {
                return "用户密码信息验证失败";
            }
            //登录日志
            await _sysLoginLogAppService.InsertLogAsync(new SysLoginLogDto { LoginName = user.LoginName, Name = user.Name, LoginType = LoginType.Cookie });
            #region 构建票据基础信息
            //创建用户claimIdentity
            var claimIdentity = this.CreateClaimsIdentity(user);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimIdentity,
                Issuer = _webAppOptions.JwtValidIssuer,
                Audience = _webAppOptions.JwtValidAudience,
                Expires = expiresAt,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            #endregion
            //生成票据
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>

        public async Task SignOutAsync(HttpContext context)
        {
            var nameIdentifierClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
            //退出
            await context.SignOutAsync();
            try
            {
                //删除权限缓存
                await _distributedCache.RemoveAsync(SysConsts.MENU_CACHEKEY_PREFIX + nameIdentifierClaim?.Value);
            }
            catch (System.Exception)
            {
            }
        }

        /// <summary>
        /// 创建用户ClaimsIdentity
        /// </summary>
        /// <returns></returns>
        private ClaimsIdentity CreateClaimsIdentity(SysUser user)
        {
            var claimIdentity = new ClaimsIdentity("Cookie");
            claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Name, user.LoginName));
            //var roleIds = await _sysUserRepository.FindUserRoleAsync(user.Id);
            //foreach (var roleId in roleIds)
            //{
            //    claimIdentity.AddClaim(new Claim(ClaimTypes.Role, roleId.ToString()));
            //}
            return claimIdentity;
        }

        /// <summary>
        /// 根据用户ID以及用户名
        /// </summary>
        /// <param name="sysUserDto"></param>
        /// <returns></returns>
        public Task<List<SysMenuDto>> GetMenusAndFunctionByUserAsync(SysUserDto sysUserDto)
        {
            return _sysMenuAppService.GetMenusAndFunctionByUserAsync(sysUserDto);
        }


    }
}
