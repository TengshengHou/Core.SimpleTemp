using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal
{
    public partial class BaseRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        ///// <summary>
        ///// 分页查询
        ///// </summary>
        ///// <param name="startPage">页码</param>
        ///// <param name="pageSize">单页数据数</param>
        ///// <param name="where">条件</param>
        ///// <param name="order">排序</param>
        ///// <returns></returns>
        //public virtual async Task<IPageModel<TEntity>> LoadPageListAsync(int startPage, int pageSize, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null, bool autoInclude = false)
        //{

        //    //var result = QueryBase();

        //    //if (where != null)
        //    //    result = result.Where(where);
        //    //if (order != null)
        //    //    result = result.OrderBy(order);
        //    //else
        //    //    result = result.OrderBy(m => m.Id);
        //    //int rowCount = await result.CountAsync();
        //    //var pageData = await result.Skip((startPage - 1) * pageSize).Take(pageSize).ToListAsync();

        //    return await LoadPageOffsetAsync((startPage - 1) * pageSize, pageSize, where, order);
        //}

        public virtual async Task<IPageModel<TEntity>> LoadPageOffsetAsync(int offset, int limit, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null)
        {
            var result = QueryBase();

            if (where != null)
                result = result.Where(where);
            if (order != null)
                result = result.OrderBy(order);
            else
                result = result.OrderBy(m => m.Id);
            int rowCount = await result.CountAsync();
            var pageData = await result.Skip(offset).Take(limit).ToListAsync();

            var PageModel = new PageModel<TEntity>()
            {
                RowCount = rowCount,
                PageData = pageData
            };
            return PageModel;
        }

        /// <summary>
        /// 该方法优化空间很大。以后有时间定要优化
        /// </summary>
        /// <param name="pTargetObjSrc"></param>
        /// <param name="pTargetObjDest"></param>
        /// <param name="noUpdateProperties"></param>
        protected void EntityToEntity(TEntity pTargetObjSrc, TEntity pTargetObjDest, List<string> noUpdateProperties = null)
        {
            foreach (var mItem in typeof(TEntity).GetProperties())
            {
                noUpdateProperties = noUpdateProperties ?? new List<string>();
                if (!noUpdateProperties.Contains(mItem.Name))
                    mItem.SetValue(pTargetObjDest, mItem.GetValue(pTargetObjSrc, new object[] { }), null);
            }
        }

        /// <summary>
        /// 根据主键构建判断表达式
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }
    }
}
