
using AutoMapper;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application
{
    public partial class BaseAppService<TDto, TEntity, TRepository> : IBaseAppService<TDto, TEntity> where TEntity : Entity where TRepository : IRepository<TEntity>
    {
        public TRepository _repository;

        public BaseAppService(TRepository repository) => _repository = repository;

        public Task DeleteAsync(TEntity entity, bool autoSave = true) => _repository.DeleteAsync(entity, autoSave);

        public Task DeleteAsync(Guid id, bool autoSave = true) => _repository.DeleteAsync(id, autoSave);

        public Task DeleteAsync(Expression<Func<TEntity, bool>> where, bool autoSave = true) => _repository.DeleteAsync(where, autoSave);

        public Task DeleteBatchAsync(List<Guid> ids, bool autoSave = true)
        {
            return this.DeleteAsync(it => ids.Contains(it.Id), autoSave);
        }

        public async Task<TDto> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool autoInclude = false) => Mapper.Map<TDto>(await _repository.FirstOrDefaultAsync(predicate, autoInclude));
        public async Task<TEntity> FirstOrDefaultEntityAsync(Expression<Func<TEntity, bool>> predicate, bool autoInclude = false) => await _repository.FirstOrDefaultAsync(predicate, autoInclude);

        public async Task<List<TDto>> GetAllListAsync(bool autoInclude = false) => Mapper.Map<List<TDto>>(await _repository.GetAllListAsync(autoInclude));

        public async Task<List<TDto>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate, bool autoInclude = false) => Mapper.Map<List<TDto>>(await _repository.GetAllListAsync(predicate, autoInclude));

        public async Task<TDto> GetAsync(Guid id, bool autoInclude = false) => Mapper.Map<TDto>(await _repository.GetAsync(id, autoInclude));
        public async Task<IPageModel<TDto>> GetAllPageListAsync(int startPage, int pageSize, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null, bool autoInclude = false)
        {
            var pageData = await _repository.LoadPageListAsync(startPage, pageSize, where, order, autoInclude);

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
            var entity = await _repository.InsertAsync(Mapper.Map<TEntity>(dto), autoSave);
            return Mapper.Map<TDto>(entity);
        }


        public async Task<TDto> UpdateAsync(TDto dto, bool autoSave = true, List<string> noUpdateProperties = null)
        {
            var entity = await _repository.UpdateAsync(Mapper.Map<TEntity>(dto), autoSave, noUpdateProperties);
            return Mapper.Map<TDto>(entity);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = true)
        {
            return await _repository.UpdateAsync(entity, autoSave);
        }

    }
}
