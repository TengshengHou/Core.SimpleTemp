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
    /// <summary>
    /// 仓储辅助
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public partial class BaseRepository<TDbContext, TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey> where TDbContext : DbContext
    {
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

        protected void GetEntityKeyInfo(TEntity entity)
        {

            foreach (var entityType in _dbContext.Model.GetEntityTypes())
            {
                System.Diagnostics.Debug.WriteLine($"entityType:{entityType.Name}");
                foreach (var key in entityType.GetKeys())//ikey:Represents a primary or alternate key on an entity.
                {
                    System.Diagnostics.Debug.WriteLine($"---key.Tostring():{key.ToString()}");
                    foreach (var property in key.Properties) //iproperty：表示实体的标量属性。
                    {
                        System.Diagnostics.Debug.WriteLine($"-------property.Name:{property.Name}");
                    }
                }
            }
        }
    }
}
