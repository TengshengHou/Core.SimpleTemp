using Microsoft.AspNetCore.Mvc;

namespace Core.SimpleTemp.Common.ActionResultHelp
{
    public static class JsonResultHelp
    {

        /// <summary>
        /// 返回基于AjaxJsonModel模型的Json字符串
        /// </summary>
        /// <param name="ajaxJsonModel"></param>
        /// <returns></returns>
        public static JsonResult Json(AjaxJsonModel ajaxJsonModel)
        {
            return new JsonResult(ajaxJsonModel);
        }

        /// <summary>
        /// 返回带有成功标识的JsonModel
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static JsonResult JsonSuccess(object data = null)
        {
            var ajaxJsonModel = new AjaxJsonModel()
            {
                Result = SysConsts.AJAX_RESULT_SUCCESS,
                Data = data
            };
            return Json(ajaxJsonModel);
        }

        /// <summary>
        /// 返回带有失败标识的JsonModel
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static JsonResult JsonFaild(string message = null)
        {
            var ajaxJsonModel = new AjaxJsonModel()
            {
                Result = SysConsts.AJAX_RESULT_FAILD,
                Message = message
            };
            return Json(ajaxJsonModel);
        }
    }
}
