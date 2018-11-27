using Core.SimpleTemp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application.RoleApp
{
    public interface ISysRoleAppService : IBaseAppService<SysRoleDto, SysRole>
    {
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
