using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Core.SimpleTemp.Common.FilterExpression;
namespace Core.SimpleTemp.Common.PagingQuery
{
    public class PagingQueryModelBuild<TEntity> : IPagingQueryModelBuild<TEntity>
    {
        #region 常量定义
        const string FILTER_CONDITION_LIST_KEY = "filterConditionList";
        const string OFFSET = "offset";
        const string LIMIT = "limit";
        #endregion

        JsonSerializer jsonSerializer = new JsonSerializer();
        PagingQueryModel<TEntity> pagingQueryModel = new PagingQueryModel<TEntity>();

        HttpContext _httpContext;

        public PagingQueryModelBuild(IHttpContextAccessor accessor)
        {
            _httpContext = accessor.HttpContext;
        }




        /// <summary>
        /// 根据当前Http请求创建PagingQuery对象
        /// </summary>
        /// <returns></returns>
        public void MakePagingQueryModel()
        {
            #region 数据验证
            if (!_httpContext.Request.Form.ContainsKey(FILTER_CONDITION_LIST_KEY))
                throw new Exception($"PagingQueryModelBuild 在请求数据中未找到{FILTER_CONDITION_LIST_KEY}");

            if (!_httpContext.Request.Form.ContainsKey(OFFSET))
                throw new Exception($"PagingQueryModelBuild 在请求数据中未找到{OFFSET}");

            if (!_httpContext.Request.Form.ContainsKey(LIMIT))
                throw new Exception($"PagingQueryModelBuild 在请求数据中未找到{LIMIT}");
            #endregion

            string filterConditionList = _httpContext.Request.Form[FILTER_CONDITION_LIST_KEY];
            int offset = int.Parse(_httpContext.Request.Form[OFFSET]);
            int limit = int.Parse(_httpContext.Request.Form[LIMIT]);

            pagingQueryModel.Limit = limit;
            pagingQueryModel.Offset = offset;
            pagingQueryModel.FilterConditionList = jsonSerializer.Deserialize<List<FilterModel>>(filterConditionList);
            pagingQueryModel.FilterExpression = ExpressionExtension.GetFilterExpression<TEntity>(pagingQueryModel.FilterConditionList);
        }

        public PagingQueryModel<TEntity> GetPaginQueryModel()
        {
            return pagingQueryModel;
        }
    }
}