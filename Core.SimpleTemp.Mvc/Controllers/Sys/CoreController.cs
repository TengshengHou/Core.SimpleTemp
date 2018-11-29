using Core.SimpleTemp.Common;
using Core.SimpleTemp.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace Core.SimpleTemp.Mvc.Controllers
{
    public class CoreController : Controller
    {

        #region JsonResult
        /// <summary>
        /// 返回基于AjaxJsonModel模型的Json字符串
        /// </summary>
        /// <param name="ajaxJsonModel"></param>
        /// <returns></returns>
        public virtual JsonResult Json(AjaxJsonModel ajaxJsonModel)
        {
            return base.Json(ajaxJsonModel);
        }

        /// <summary>
        /// 返回带有成功标识的JsonModel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public virtual JsonResult JsonSuccess(object data = null)
        {
            var ajaxJsonModel = new AjaxJsonModel()
            {
                Result = SysConsts.AJAX_RESULT_SUCCESS,
                Data = data
            };
            return this.Json(ajaxJsonModel);
        }

        /// <summary>
        /// 返回带有失败标识的JsonModel
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual JsonResult JsonFaild(string message = null)
        {
            var ajaxJsonModel = new AjaxJsonModel()
            {
                Result = SysConsts.AJAX_RESULT_FAILD,
                Message = message
            };
            return this.Json(ajaxJsonModel);
        }
        #endregion

    }
}
