
using AutoMapper;
using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories;
using Core.SimpleTemp.Domain.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.Internal.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Service
{
    public class BaseAppService<TDto, TEntity, TRepository> : IBaseAppService<TDto, TEntity> where TEntity : Entity where TRepository : IRepository<TEntity>
    {
        public TRepository _repository;

        public BaseAppService(TRepository repository) => _repository = repository;

        public Task DeleteAsync(TEntity entity, bool autoSave = true) => _repository.DeleteAsync(entity, autoSave);

        public Task DeleteAsync(Guid id, bool autoSave = true) => _repository.DeleteAsync(id, autoSave);

        public Task DeleteAsync(Expression<Func<TEntity, bool>> where, bool autoSave = true) => _repository.DeleteAsync(where, autoSave);

        public Task DeleteBatchAsync(List<Guid> ids)
        {
            return this.DeleteAsync(it => ids.Contains(it.Id));
        }

        public async Task<TDto> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => Mapper.Map<TDto>(await _repository.FirstOrDefaultAsync(predicate));

        public async Task<List<TDto>> GetAllListAsync() => Mapper.Map<List<TDto>>(await _repository.GetAllListAsync());

        public async Task<List<TDto>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate) => Mapper.Map<List<TDto>>(await _repository.GetAllListAsync(predicate));

        public async Task<TDto> GetAsync(Guid id) => Mapper.Map<TDto>(await _repository.GetAsync(id));
        public async Task<IPageModel<TDto>> GetAllPageListAsync(int startPage, int pageSize, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null)
        {
            var pageData = await _repository.LoadPageListAsync(startPage, pageSize, where, order);

            IPageModel<TDto> pageModelDto = new PageModel<TDto>()
            {
                PageData = Mapper.Map<List<TDto>>(pageData.PageData)
                ,
                RowCount = pageData.RowCount
            };
            return pageModelDto;
        }

        public async Task<TDto> InsertAsync(TDto dto, bool autoSave = true)
        {
            var entity = await _repository.InsertAsync(Mapper.Map<TEntity>(dto));
            return Mapper.Map<TDto>(entity);
        }

        public async Task<TDto> UpdateAsync(TDto dto, bool autoSave = true)
        {
            var entity = await _repository.UpdateAsync(Mapper.Map<TEntity>(dto));
            return Mapper.Map<TDto>(entity);
        }
    }
}
