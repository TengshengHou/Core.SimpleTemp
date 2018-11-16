using Core.SimpleTemp.Domain.IRepositories.Internal.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Service
{
    public interface IBaseAppService<TDto, TEntity, TPrimaryKey>
    {
        Task<List<TDto>> GetAllListAsync();

        Task<List<TDto>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TDto> GetAsync(TPrimaryKey id);
        Task<TDto> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TDto> InsertAsync(TDto dto, bool autoSave = true);
        Task<TDto> UpdateAsync(TDto entity, bool autoSave = true);

        Task DeleteAsync(TEntity entity, bool autoSave = true);
        Task DeleteAsync(TPrimaryKey id, bool autoSave = true);
        Task DeleteAsync(Expression<Func<TEntity, bool>> where, bool autoSave = true);
        Task DeleteBatchAsync(List<Guid> ids);

        Task<IPageModel<TDto>> GetAllPageListAsync(int startPage, int pageSize, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null);


    }

    public interface IBaseAppService<TDto, TEntity> : IBaseAppService<TDto, TEntity, Guid>
    {
    }
}
