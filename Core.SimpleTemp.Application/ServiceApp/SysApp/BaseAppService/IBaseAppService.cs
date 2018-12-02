using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application
{
    public partial interface  IBaseAppService<TDto, TEntity, TPrimaryKey>
    {
        Task<List<TDto>> GetAllListAsync( bool autoInclude = false);

        Task<List<TDto>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate, bool autoInclude = false);
        Task<TDto> GetAsync(TPrimaryKey id, bool autoInclude = false);
        Task<TDto> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool autoInclude = false);
        Task<TEntity> FirstOrDefaultEntityAsync(Expression<Func<TEntity, bool>> predicate, bool autoInclude = false) ;

        Task<TDto> InsertAsync(TDto dto, bool autoSave = true);
        Task<TDto> UpdateAsync(TDto dto, bool autoSave = true, List<string> noUpdateProperties = null);
        Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = true);


        Task DeleteAsync(TEntity entity, bool autoSave = true);
        Task DeleteAsync(TPrimaryKey id, bool autoSave = true);
        Task DeleteAsync(Expression<Func<TEntity, bool>> where, bool autoSave = true);
        Task DeleteBatchAsync(List<Guid> ids, bool autoSave = true);

        Task<IPageModel<TDto>> GetAllPageListAsync(int startPage, int pageSize, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null, bool autoInclude = false);


    }

    public interface IBaseAppService<TDto, TEntity> : IBaseAppService<TDto, TEntity, Guid>
    {
    }
}
