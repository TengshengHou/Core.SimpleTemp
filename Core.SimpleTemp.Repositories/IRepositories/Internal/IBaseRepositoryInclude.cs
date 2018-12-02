using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Repositories.IRepositories
{
    public partial interface IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {

        /// <summary>
        /// 获取所以列表
        /// </summary>
        /// <param name="navigationproperty">需要加载的导航属性</param>
        /// <returns></returns>
        Task<List<TEntity>> IGetAllListAsync(string[] navigationproperty);

        /// <summary>
        /// 根据lambda表达式条件获取实体集合
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <param name="navigationproperty">需要加载的导航属性</param>
        /// <returns></returns>
        Task<List<TEntity>> IGetAllListAsync(Expression<Func<TEntity, bool>> predicate, string[] navigationproperty);


        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">实体主键</param>
        /// <param name="navigationproperty">需要加载的导航属性</param>
        /// <returns></returns>
        Task<TEntity> IGetAsync(TPrimaryKey id, string[] navigationproperty);


        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <param name="navigationproperty">需要加载的导航属性</param>
        /// <returns></returns>
        Task<TEntity> IFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string[] navigationproperty);


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="startPage">页码</param>
        /// <param name="pageSize">单页数据数</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <param name="navigationproperty">需要加载的导航属性</param>
        /// <returns></returns>
        Task<IPageModel<TEntity>> ILoadPageListAsync(int startPage, int pageSize, string[] navigationproperty, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null);


    }
}
