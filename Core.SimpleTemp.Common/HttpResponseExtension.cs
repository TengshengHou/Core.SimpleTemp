using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Core.SimpleTemp.Common
{
    public static class HttpResponseExtension
    {



        public static StringBuilder CreateAjaxResponseExceptionJson(this HttpResponse httpResponse, string errorMsg)
        {

            StringBuilder sb = new StringBuilder(16 * 3);
            sb.Append("{");
            sb.Append($" \"result\":\"{SysConsts.AJAX_RESULT_FAILD}\" ,");
            sb.Append($" \"message\":\"{errorMsg}\" ");
            sb.Append("}");
            return sb;
        }
    }
}
