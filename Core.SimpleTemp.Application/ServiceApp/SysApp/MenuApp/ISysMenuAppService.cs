using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using Core.SimpleTemp.Application.UserApp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.SimpleTemp.Common;

namespace Core.SimpleTemp.Application.MenuApp
{
    [AutoDi(typeof(SysMenuAppService))]
    public interface ISysMenuAppService : IBaseAppService<SysMenuDto, SysMenu>
    {
        /// <summary>
        /// 根据父级Id获取功能列表
        /// </summary>
        /// <param name="parentId">父级Id</param>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="rowCount">数据总数</param>
        /// <returns></returns>
        Task<IPageModel<SysMenuDto>> GetMenusByParentAsync(Guid parentId, int startPage, int pageSize);

        Task<List<SysMenuDto>> GetMenusAndFunctionByUserAsync(SysUserDto sysUserDto);

        Task<bool> IsNoneChildren(List<Guid> ids);


    }
}
