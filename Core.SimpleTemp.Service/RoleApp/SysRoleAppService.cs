using AutoMapper;
using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories;
using Core.SimpleTemp.Domain.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.Internal.Data;
using Core.SimpleTemp.Service.MenuApp;
using Core.SimpleTemp.Service.RoleApp.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Service.RoleApp
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
