using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal.Data;

namespace Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal
{
    /// <summary>
    /// 使用仓储查询可指定Include属性
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public partial class BaseRepository<TDbContext, TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey> where TDbContext : DbContext
    {
        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        public virtual Task<List<TEntity>> IGetAllListAsync(string[] navigationproperty)
        {

            var queryable = _dbContext.Set<TEntity>().AsQueryable();
            AutoInclude(ref queryable, navigationproperty);
            return queryable.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// 根据lambda表达式条件获取实体集合
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public virtual Task<List<TEntity>> IGetAllListAsync(Expression<Func<TEntity, bool>> predicate, string[] navigationproperty)
        {
            var queryable = _dbContext.Set<TEntity>().AsQueryable();
            AutoInclude(ref queryable, navigationproperty);
            return queryable.Where(predicate).AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        public virtual Task<TEntity> IGetAsync(TPrimaryKey id, string[] navigationproperty)
        {
            var queryable = _dbContext.Set<TEntity>().AsQueryable();
            AutoInclude(ref queryable, navigationproperty);
            return queryable.FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public virtual Task<TEntity> IFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string[] navigationproperty)
        {
            var queryable = _dbContext.Set<TEntity>().AsQueryable();
            AutoInclude(ref queryable, navigationproperty);
            return queryable.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IPageModel<TEntity>> ILoadPageOffsetAsync(int offset, int limit, string[] navigationproperty, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null)
        {
            var result = _dbContext.Set<TEntity>().AsQueryable();
            AutoInclude(ref result, navigationproperty);

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

        protected void IncludeAll(ref IQueryable<TEntity> queryable)
        {
            if (_properties != null && _properties.Any())
                foreach (var item in _properties)
                {
                    queryable = queryable.Include(item);
                }
        }

        protected void AutoInclude(ref IQueryable<TEntity> queryable, string[] navigationproperty)
        {
            if (navigationproperty == null)
                return;
            if (!navigationproperty.Any())
                return;
            foreach (var item in navigationproperty)
            {
                queryable = (IQueryable<TEntity>)queryable.Include(item);
            }
        }
    }
}
