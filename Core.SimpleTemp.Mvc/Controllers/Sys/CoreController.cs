﻿using Core.SimpleTemp.Application.Authorization;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        public async System.Threading.Tasks.Task AuthorizeListAsync(string[] functionCodes)
        {

            var authorizationService = (IAuthorizationService)HttpContext.RequestServices.GetService(typeof(IAuthorizationService));
            var dic = await authorizationService.AuthorizeListAsync(functionCodes, User);
            JsonSerializer sj = new JsonSerializer();
            var strJson = sj.Serialize(dic);
            ViewBag.AuthorizeList = strJson;
        }
    }
}
