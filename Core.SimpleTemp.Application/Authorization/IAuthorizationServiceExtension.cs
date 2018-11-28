using Core.SimpleTemp.Common;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application.Authorization
{

    public static class IAuthorizationServiceExtension
    {
        public static async Task<bool> AuthorizeAsync(this IAuthorizationService authorizationService, string functionCode, ClaimsPrincipal user)
        {
            return (await authorizationService.AuthorizeAsync(user, null, new PermissionAuthorizationRequirement(functionCode))).Succeeded;
        }
    }
}
