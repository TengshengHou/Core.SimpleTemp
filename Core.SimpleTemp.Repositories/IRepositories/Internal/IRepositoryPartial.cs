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
        //Task<IPageModel<TEntity>> LoadPageListAsync(int startPage, int pageSize, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null);
        Task<IPageModel<TEntity>> LoadPageOffsetAsync(int offset, int limit, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null);
    }
}
