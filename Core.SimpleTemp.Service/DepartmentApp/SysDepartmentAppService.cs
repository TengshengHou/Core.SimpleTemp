using AutoMapper;
using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories;
using Core.SimpleTemp.Domain.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.Internal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Core.SimpleTemp.Service
{
    public class SysDepartmentAppService : ISysDepartmentAppService
    {
        private readonly ISysDepartmentRepository _repository;
        public SysDepartmentAppService(ISysDepartmentRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<SysDepartmentDto>> GetAllListAsync()
        {
            var list = Mapper.Map<List<SysDepartmentDto>>(await _repository.GetAllListAsync(it => it.Id != Guid.Empty));
            return list.OrderBy(it => it.Code).ToList();
        }

        /// <summary>
        /// 根据父级Id获取子级列表
        /// </summary>
        /// <param name="parentId">父级Id</param>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public async Task<IPageModel<SysDepartmentDto>> GetChildrenByParentAsync(Guid parentId, int startPage, int pageSize)
        {
            var pageModel = await _repository.LoadPageListAsync(startPage, pageSize, it => it.ParentId == parentId, it => it.Code);
            IPageModel<SysDepartmentDto> viewPageModel = new PageModel<SysDepartmentDto>
            {
                PageData = Mapper.Map<List<SysDepartmentDto>>(pageModel.PageData),
                RowCount = pageModel.RowCount
            };
            return viewPageModel;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        public async Task<bool> InsertAsync(SysDepartmentDto dto)
        {
            var menu = await _repository.InsertAsync(Mapper.Map<SysDepartment>(dto));
            return menu == null ? false : true;
        }

        public async Task<SysDepartmentDto> UpdateAsync(SysDepartmentDto dto)
        {
            var entity = await _repository.UpdateAsync(Mapper.Map<SysDepartment>(dto));
            var retDto = Mapper.Map<SysDepartmentDto>(entity);
            return retDto;

        }

        /// <summary>
        /// 根据Id集合批量删除
        /// </summary>
        /// <param name="ids">Id集合</param>
        public async Task DeleteBatchAsync(List<Guid> ids)
        {
            await _repository.DeleteAsync(it => ids.Contains(it.Id));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">Id</param>
        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public async Task<SysDepartmentDto> GetAsync(Guid id)
        {
            return Mapper.Map<SysDepartmentDto>(await _repository.GetAsync(id));
        }
    }
}
