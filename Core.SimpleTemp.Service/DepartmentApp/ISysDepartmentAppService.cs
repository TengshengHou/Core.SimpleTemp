using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories.Internal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Service
{
    public interface ISysDepartmentAppService : IBaseAppService<SysDepartmentDto, SysDepartment>
    {

        /// <summary>
        /// 根据父级Id获取子级列表
        /// </summary>
        /// <param name="parentId">父级Id</param>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        Task<IPageModel<SysDepartmentDto>> GetChildrenByParentAsync(Guid parentId, int startPage, int pageSize);
    }
}
