using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories.Internal.Data;
using Core.SimpleTemp.Service.MenuApp;
using Core.SimpleTemp.Service.RoleApp.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SysRoleDto = Core.SimpleTemp.Service.MenuApp.SysRoleDto;

namespace Core.SimpleTemp.Service.RoleApp
{
    public interface ISysRoleAppService : IBaseAppService<SysRoleDto, SysRole>
    {


        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="startPage">起始页</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="rowCount">数据总数</param>
        /// <returns></returns>
        Task<IPageModel<SysRoleDto>> GetAllPageListAsync(int startPage, int pageSize);


        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        Task<List<Guid>> GetMenuListByRoleAsync(Guid roleId);

        /// <summary>
        /// 更新角色权限关联关系
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="roleMenus">角色权限集合</param>
        /// <returns></returns>
        Task<bool> UpdateRoleMenuAsync(Guid roleId, List<SysRoleMenuDto> roleMenus);

    }
}
