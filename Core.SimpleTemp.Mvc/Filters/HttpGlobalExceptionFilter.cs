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
        public void OnException(ExceptionContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest())
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
