using Core.SimpleTemp.Application.Authorization;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Common.ActionResultHelp;
using Core.SimpleTemp.Common.PagingQuery;
using Core.SimpleTemp.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.SimpleTemp.Mvc.Controllers
{
    public class CoreController<TEntity> : Controller
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

        public async System.Threading.Tasks.Task AuthorizeListAsync(string[] functionCodes)
        {

            var authorizationService = (IAuthorizationService)HttpContext.RequestServices.GetService(typeof(IAuthorizationService));
            var dic = await authorizationService.AuthorizeListAsync(functionCodes, User);
            JsonSerializer sj = new JsonSerializer();
            var strJson = sj.Serialize(dic);
            ViewBag.AuthorizeList = strJson;
        }

        public Guid[] Str2GuidArray(string strGuids)
        {
            string[] idArray = strGuids.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var idCount = idArray.Length;
            Guid[] guidArray = new Guid[idArray.Length];
            for (int i = 0; i < idCount; i++)
            {
                guidArray[i] = Guid.Parse(idArray[i]);
            }
            return guidArray;
        }

        public PagingQueryModel<TEntity> GetPagingQueryModel()
        {
            var pagingQueryModelBuild = new PagingQueryModelBuild<TEntity>(HttpContext);
            return pagingQueryModelBuild.Build();
        }

    }
}
