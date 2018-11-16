using Core.SimpleTemp.Domain.IRepositories.Internal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Service
{
    public interface ISysDepartmentAppService
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<List<SysDepartmentDto>> GetAllListAsync();

        /// <summary>
        /// 根据父级Id获取子级列表
        /// </summary>
        /// <param name="parentId">父级Id</param>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        Task<IPageModel<SysDepartmentDto>> GetChildrenByParentAsync(Guid parentId, int startPage, int pageSize);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        Task<bool> InsertAsync(SysDepartmentDto dto);

        Task<SysDepartmentDto> UpdateAsync(SysDepartmentDto dto);

        /// <summary>
        /// 根据Id集合批量删除
        /// </summary>
        /// <param name="ids">Id集合</param>
       Task DeleteBatchAsync(List<Guid> ids);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">Id</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        Task<SysDepartmentDto> GetAsync(Guid id);
    }
}
