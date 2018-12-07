using Core.SimpleTemp.Common.FilterExpression;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.SimpleTemp.Common
{
    /// <summary>
    /// 分页过来查询对应
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PagingQueryModel<TEntity>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        /// <summary>
        /// 过滤条件
        /// </summary>
        public List<FilterModel> FilterConditionList { get; set; }
        /// <summary>
        /// 编译生成的过滤Expression表达式
        /// </summary>
        public Expression<Func<TEntity, bool>> FilterExpression { get; set; }

    }


}
