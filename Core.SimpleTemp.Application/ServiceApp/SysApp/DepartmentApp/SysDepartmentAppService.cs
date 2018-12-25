using AutoMapper;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace Core.SimpleTemp.Application
{
    public class SysDepartmentAppService : BaseAppService<SysDepartmentDto, SysDepartment, ISysDepartmentRepository>, ISysDepartmentAppService
    {
        public SysDepartmentAppService(ISysDepartmentRepository repository) : base(repository)
        {
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

            //var pageModel = await GetAllPageListAsync(startPage, pageSize, it => it.ParentId == parentId, it => it.Code);
            var pageModel = await GetAllPageListAsync(startPage, pageSize, it => it.ParentId == parentId, it => it.Code);
            IPageModel<SysDepartmentDto> viewPageModel = new PageModel<SysDepartmentDto>
            {
                PageData = Mapper.Map<List<SysDepartmentDto>>(pageModel.PageData),
                RowCount = pageModel.RowCount
            };
            return viewPageModel;
        }

        public async Task<bool> IsNoneChildren(Guid[] ids)
        {
            var delEntity = await _repository.FirstOrDefaultAsync(dep => ids.Contains(dep.ParentId));
            if (delEntity != null)
                return false;
            return true;
        }

    }
}
