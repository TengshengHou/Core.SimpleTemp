using AutoMapper;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application
{
    public partial class BaseAppService<TDto, TEntity, TRepository> : IBaseAppService<TDto, TEntity> where TEntity : Entity where TRepository : IRepository<TEntity>
    {

        public async Task<TDto> IFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string[] navigationproperty) => Mapper.Map<TDto>(await _repository.IFirstOrDefaultAsync(predicate, navigationproperty));
        public async Task<TEntity> IFirstOrDefaultEntityAsync(Expression<Func<TEntity, bool>> predicate, string[] navigationproperty) => await _repository.IFirstOrDefaultAsync(predicate, navigationproperty);

        public async Task<List<TDto>> IGetAllListAsync(string[] navigationproperty) => Mapper.Map<List<TDto>>(await _repository.IGetAllListAsync(navigationproperty));

        public async Task<List<TDto>> IGetAllListAsync(Expression<Func<TEntity, bool>> predicate, string[] navigationproperty) => Mapper.Map<List<TDto>>(await _repository.IGetAllListAsync(predicate, navigationproperty));

        public async Task<TDto> IGetAsync(Guid id, string[] navigationproperty) => Mapper.Map<TDto>(await _repository.IGetAsync(id, navigationproperty));
        public async Task<IPageModel<TDto>> IGetAllPageListAsync(int startPage, int pageSize, string[] navigationproperty, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null)
        {
            var pageModelDto = await ILoadPageOffsetAsync((startPage - 1) * pageSize, pageSize, navigationproperty, where, order);
            return pageModelDto;
        }
        public async Task<IPageModel<TDto>> ILoadPageOffsetAsync(int offset, int limit, string[] navigationproperty, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null)
        {
            var pageModelEntity = await _repository.ILoadPageOffsetAsync(offset, limit, navigationproperty, where, order);
            IPageModel<TDto> pageModelDto = new PageModel<TDto>()
            {
                PageData = Mapper.Map<List<TDto>>(pageModelEntity.PageData)
               ,
                RowCount = pageModelEntity.RowCount
            };
            return pageModelDto;

        }
    }
}
