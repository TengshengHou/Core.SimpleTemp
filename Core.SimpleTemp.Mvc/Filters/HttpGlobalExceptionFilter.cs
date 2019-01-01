using Core.SimpleTemp.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Mvc.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            //只处理Ajax 其他交给 异常中间件处理
            if (context.HttpContext.Request.IsAjaxRequest() || context.HttpContext.Request.Path.Value.StartsWith("/api/"))
            {
                context.HttpContext.Response.StatusCode = 200;
                context.HttpContext.Response.ContentType = "application/json;charset=utf-8";
                var sb = context.HttpContext.Response.CreateAjaxResponseExceptionJson(context.Exception.Message);
                context.HttpContext.Response.WriteAsync(sb.ToString()).GetAwaiter().GetResult();
                context.ExceptionHandled = true;
            }
        }
    }
}
