using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Common
{
    public static class HttpRequestExtension
    {
        /// <summary>
        /// 判断请求是否是Ajax
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            //参考Security/src/Microsoft.AspNetCore.Authentication.Cookies/Events/CookieAuthenticationEvents.cs
            return string.Equals(request.Query["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal) ||
                string.Equals(request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal);
        }

        
    }
}
