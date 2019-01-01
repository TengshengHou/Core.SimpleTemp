using Core.SimpleTemp.Common.ActionResultHelp;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Mvc.Api
{
    public class CoreApiController: ControllerBase
    {
        #region JsonResult

        /// <summary>
        /// 返回带有成功标识的JsonModel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual JsonResult JsonSuccess(object data = null)
        {
            return JsonResultHelp.JsonSuccess(data);
        }

        /// <summary>
        /// 返回带有失败标识的JsonModel
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual JsonResult JsonFaild(string message = null)
        {
            return JsonResultHelp.JsonSuccess(message);
        }
        #endregion
    }
}
