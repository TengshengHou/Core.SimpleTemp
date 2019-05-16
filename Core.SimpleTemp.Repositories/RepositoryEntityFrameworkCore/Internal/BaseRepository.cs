using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public partial class BaseRepository<TDbContext, TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey> where TDbContext : DbContext
    {
        //定义数据访问上下文对象
        public readonly TDbContext _dbContext;
        public readonly IEnumerable<string> _properties;
        /// <summary>
        /// 通过构造函数注入得到数据上下文对象实例
        /// </summary>
        /// <param name="dbContext"></param>
        public BaseRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
            //暂时不用Include
            //_properties = EntityInfos.GetEntityInfo(typeof(TEntity).FullName)?.Select(c => c.Name);
        }


        public virtual IQueryable<TEntity> QueryBase()
        {
            GetEntityKeyInfo(null);
            return _dbContext.Set<TEntity>();
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        public virtual Task<List<TEntity>> GetAllListAsync()
        {
            return QueryBase().AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// 根据lambda表达式条件获取实体集合
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return QueryBase().Where(predicate).AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <returns></returns>
        public virtual Task<TEntity> GetAsync(TPrimaryKey id)
        {
            return QueryBase().FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return QueryBase().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <returns></returns>
        public virtual async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = true)
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
        public virtual async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = true, List<string> noUpdateProperties = null)
        {
            var obj = await GetAsync(entity.Id);
            EntityToEntity(entity, obj, noUpdateProperties);
            if (autoSave)
                await SaveAsync();
            return obj;
        }

        /// <summary>
        /// 更新实体(待优化)
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        public virtual async Task<List<TEntity>> UpdateAsync(List<TEntity> entitys, bool autoSave = true, List<string> noUpdateProperties = null)
        {
            List<TEntity> updateList = new List<TEntity>();
            foreach (var entity in entitys)
            {
                var obj = await GetAsync(entity.Id);
                if (!object.Equals(noUpdateProperties, null))
                    EntityToEntity(entity, obj, noUpdateProperties);
                updateList.Add(obj);
            }
            if (autoSave)
                await SaveAsync();
            return updateList;
        }


        ///// <summary>
        ///// 更新实体（需要先从EF中查询出来再做更新)
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="autoSave"></param>
        ///// <returns></returns>
        //public virtual async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = true)
        //{
        //    if (autoSave)
        //        await SaveAsync();
        //    return entity;
        //}

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        public virtual async Task DeleteAsync(TEntity entity, bool autoSave = true)
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
        public virtual async Task DeleteAsync(TPrimaryKey id, bool autoSave = true)
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
        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> where, bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Where(where).ToList().ForEach(it => _dbContext.Set<TEntity>().Remove(it));
            if (autoSave)
                await SaveAsync();
        }

        /// <summary>
        /// 事务性保存
        /// </summary>
        public virtual Task<int> SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }

    /// <summary>
    /// 主键为Guid 的指定仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseRepository<TDbContext, TEntity> : BaseRepository<TDbContext, TEntity, Guid> where TEntity : Entity where TDbContext : DbContext
    {
        public BaseRepository(TDbContext dbContext) : base(dbContext)
        {
        }
    }


    /// <summary>
    /// 主键为Guid 的默认仓储库
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class BaseRepository<TEntity> : BaseRepository<CoreDBContext, TEntity> where TEntity : Entity
    {
        public BaseRepository(CoreDBContext dbContext) : base(dbContext)
        {
        }
    }

}
