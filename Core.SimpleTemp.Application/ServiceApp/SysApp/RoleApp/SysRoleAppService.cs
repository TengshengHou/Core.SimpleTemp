using AutoMapper;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application.RoleApp
{
   
    public class SysRoleAppService : BaseAppService<SysRoleDto, SysRole, ISysRoleRepository>, ISysRoleAppService
    {
        public SysRoleAppService(ISysRoleRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <returns></returns>
        public async Task<List<Guid>> GetMenuListByRoleAsync(Guid roleId)
        {
            return await _repository.GetMenuListByRoleAsync(roleId);
        }

        /// <summary>
        /// 更新角色权限关联关系
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="roleMenus">角色权限集合</param>
        /// <returns></returns>
        public async Task<bool> UpdateRoleMenuAsync(Guid roleId, List<SysRoleMenuDto> roleMenus)
        {
            return await _repository.UpdateRoleMenuAsync(roleId, Mapper.Map<List<SysRoleMenu>>(roleMenus));
        }

    }
}
