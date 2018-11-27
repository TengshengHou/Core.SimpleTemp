using Core.SimpleTemp.Application.UserApp;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionAuthorizationRequirement>
    {

        ISysLoginService _sysLoginService;
        public PermissionAuthorizationHandler(ISysLoginService sysLoginService)
        {
            _sysLoginService = sysLoginService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement)
        {
            if (context.User != null)
            {
                //从当前票据获取用户信息
                var nameIdentifierClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
                var id = nameIdentifierClaim.Value;
                var userDto = new SysUserDto() { Id = Guid.Parse(id), LoginName = context.User.Identity.Name };
                //确认是否是管理员
                if (context.User.Identity.Name.Equals("admin"))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    //验证权限
                    var userFunctionList = await _sysLoginService.GetMenusAndFunctionByUserAsync(userDto);
                    var serCodeList = from function in userFunctionList select function.Code;
                    if (userFunctionList != null && userFunctionList.Any())
                    {
                        if (serCodeList.Contains(requirement.FunctionCode))
                            context.Succeed(requirement);
                    }
                }
            }
        }
    }
}
