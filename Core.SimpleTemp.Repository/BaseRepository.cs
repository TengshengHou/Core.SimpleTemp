using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories;
using Core.SimpleTemp.Domain.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.Internal.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Repository
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public abstract partial class BaseRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        //定义数据访问上下文对象
        public readonly CoreDBContext _dbContext;

        /// <summary>
        /// 通过构造函数注入得到数据上下文对象实例
        /// </summary>
        /// <param name="dbContext"></param>
        public BaseRepository(CoreDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        public Task<List<TEntity>> GetAllListAsync()
        {
            return _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// 根据lambda表达式条件获取实体集合
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        public Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return _dbContext.Set<TEntity>().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <returns></returns>
        public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = true)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            if (autoSave)
                await SaveAsync();
            return entity;
        }

        /// <summary>
        /// 更新实体(待优化)
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = true)
        {
            var obj = await GetAsync(entity.Id);
            EntityToEntity(entity, obj);
            if (autoSave)
                await SaveAsync();
            return entity;
        }

        private void EntityToEntity<T>(T pTargetObjSrc, T pTargetObjDest)
        {
            foreach (var mItem in typeof(T).GetProperties())
            {
                mItem.SetValue(pTargetObjDest, mItem.GetValue(pTargetObjSrc, new object[] { }), null);
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        public async Task DeleteAsync(TEntity entity, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            if (autoSave)
                await SaveAsync();
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <param name="autoSave">是否立即执行保存</param>
        public async Task DeleteAsync(TPrimaryKey id, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Remove(await GetAsync(id));
            if (autoSave)
                await SaveAsync();
        }

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="where">lambda表达式</param>
        /// <param name="autoSave">是否自动保存</param>
        public async Task DeleteAsync(Expression<Func<TEntity, bool>> where, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Where(where).ToList().ForEach(it => _dbContext.Set<TEntity>().Remove(it));
            if (autoSave)
                await SaveAsync();
        }

        /// <summary>
        /// 事务性保存
        /// </summary>
        public Task<int> SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
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


    /// <summary>
    /// 主键为int类型的仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class BaseRepository<TEntity> : BaseRepository<TEntity, Guid> where TEntity : Entity
    {
        public BaseRepository(CoreDBContext dbContext) : base(dbContext)
        {
        }
    }

}
