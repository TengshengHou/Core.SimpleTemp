using Core.SimpleTemp.Domain.IRepositories.Internal.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Service.MenuApp
{
    public interface ISysMenuAppService
    {
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <returns></returns>
        Task<List<SysMenuDto>> GetAllListAsync();

        /// <summary>
        /// 根据父级Id获取功能列表
        /// </summary>
        /// <param name="parentId">父级Id</param>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="rowCount">数据总数</param>
        /// <returns></returns>
        Task<IPageModel<SysMenuDto>> GetMenusByParentAsync(Guid parentId, int startPage, int pageSize);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto">实体</param>
        /// <returns></returns>
        Task<bool> InsertAsync(SysMenuDto dto);

        /// <summary>
        /// 根据Id集合批量删除
        /// </summary>
        /// <param name="ids">功能Id集合</param>
        Task DeleteBatchAsync(List<Guid> ids);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">功能Id</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// 根据Id获取实体
        /// </summary>
        /// <param name="id">功能Id</param>
        /// <returns></returns>
        Task<SysMenuDto> GetAsync(Guid id);

        Task<SysMenuDto> UpdateAsync(SysMenuDto entity, bool autoSave = true);
    }
}
