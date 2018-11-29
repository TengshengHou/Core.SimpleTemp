using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Application.Authorization
{
    /// <summary>
    /// 自定义授权Requirement
    /// </summary>
    public class PermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public PermissionAuthorizationRequirement(string functionCode)
        {
            FunctionCode = functionCode;
        }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string FunctionCode { get; set; }
    }
}
