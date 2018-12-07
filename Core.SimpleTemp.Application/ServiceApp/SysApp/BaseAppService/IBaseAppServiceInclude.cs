using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application
{
    public partial interface IBaseAppService<TDto, TEntity, TPrimaryKey>
    {
        Task<TDto> IFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string[] navigationproperty);
        Task<TEntity> IFirstOrDefaultEntityAsync(Expression<Func<TEntity, bool>> predicate, string[] navigationproperty);

        Task<List<TDto>> IGetAllListAsync(string[] navigationproperty);

        Task<List<TDto>> IGetAllListAsync(Expression<Func<TEntity, bool>> predicate, string[] navigationproperty);

        Task<TDto> IGetAsync(Guid id, string[] navigationproperty);
        Task<IPageModel<TDto>> IGetAllPageListAsync(int startPage, int pageSize, string[] navigationproperty, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null);
        Task<IPageModel<TDto>> ILoadPageOffsetAsync(int offset, int limit, string[] navigationproperty, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null);
    }
}
