using AutoMapper;
using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories;
using Core.SimpleTemp.Domain.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.Internal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
            var pageModel = await _repository.LoadPageListAsync(startPage, pageSize, it => it.ParentId == parentId, it => it.Code);
            IPageModel<SysDepartmentDto> viewPageModel = new PageModel<SysDepartmentDto>
            {
                PageData = Mapper.Map<List<SysDepartmentDto>>(pageModel.PageData),
                RowCount = pageModel.RowCount
            };
            return viewPageModel;
        }

        public async Task<bool> IsNoneChildren(List<Guid> ids)
        {
            foreach (var item in ids)
            {
                var delEntity = await FirstOrDefaultAsync(e => e.ParentId == item);
                if (delEntity != null)
                    return false;
            }
            return true;
        }

    }
}
