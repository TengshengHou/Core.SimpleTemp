using Core.SimpleTemp.Common;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application.Authorization
{
    /// <summary>
    /// 授权扩展
    /// </summary>
    public static class IAuthorizationServiceExtension
    {
        /// <summary>
        /// 手动获取授权结果，多次调用只消耗一次性能。
        /// </summary>
        /// <param name="authorizationService"></param>
        /// <param name="functionCode"></param>
        /// <param name="user"></param>
        /// <returns>是否已授权</returns>
        public static async Task<bool> AuthorizeAsync(this IAuthorizationService authorizationService, string functionCode, ClaimsPrincipal user)
        {
            return (await authorizationService.AuthorizeAsync(user, null, new PermissionAuthorizationRequirement(functionCode))).Succeeded;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorizationService"></param>
        /// <param name="functionCodes"></param>
        /// <param name="user"></param>
        /// <returns></returns>

        public static async Task<Dictionary<string, bool>> AuthorizeListAsync(this IAuthorizationService authorizationService, string[] functionCodes, ClaimsPrincipal user)
        {
            var dic = new Dictionary<string, bool>();
            foreach (var functionCode in functionCodes)
            {
                bool succeeded = false;
                try
                {
                    succeeded = await authorizationService.AuthorizeAsync(functionCode, user);
                }
                catch (Exception)
                {
                    succeeded = false;
                }
                dic.Add(functionCode, succeeded);
            }
            return dic;
        }
    }
}
