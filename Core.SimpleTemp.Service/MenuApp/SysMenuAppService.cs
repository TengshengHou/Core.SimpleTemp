using AutoMapper;
using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.SimpleTemp.Repository;
using Core.SimpleTemp.Domain.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.Internal.Data;
using Core.SimpleTemp.Service.UserApp.Dto;

namespace Core.SimpleTemp.Service.MenuApp
{
    public class SysMenuAppService : BaseAppService<SysMenuDto, SysMenu, ISysMenuRepository>, ISysMenuAppService
    {
        private readonly ISysMenuRepository _sysMenuRepository;
        private readonly ISysUserRepository _sysUserRepository;
        private readonly ISysRoleRepository _sysRoleRepository;
        public SysMenuAppService(ISysMenuRepository sysMenuRepository, ISysUserRepository sysUserRepository, ISysRoleRepository sysRoleRepository) : base(sysMenuRepository)
        {
            _sysMenuRepository = sysMenuRepository;
            _sysUserRepository = sysUserRepository;
            _sysRoleRepository = sysRoleRepository;
        }


        public async Task<IPageModel<SysMenuDto>> GetMenusByParentAsync(Guid parentId, int startPage, int pageSize)
        {
            var pageMenu = await _sysMenuRepository.LoadPageListAsync(startPage, pageSize, it => it.ParentId == parentId, it => it.SerialNumber);
            IPageModel<SysMenuDto> pageModel = new PageModel<SysMenuDto>()
            {
                PageData = Mapper.Map<List<SysMenuDto>>(pageMenu.PageData),
                RowCount = pageMenu.RowCount
            };
            return pageModel;
        }

        /// <summary>
        /// 根据用户获取功能菜单
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public async Task<List<SysMenuDto>> GetMenusByUserAsync(SysUserDto sysUserDto)
        {
            //查询出系统所有菜单
            List<SysMenuDto> result = new List<SysMenuDto>();
            var allMenus = await _sysMenuRepository.GetAllListAsync(it => it.Type == 0);
            allMenus = allMenus.OrderBy(it => it.SerialNumber).ToList();

            if (sysUserDto.LoginName == "admin") //超级管理员
                return Mapper.Map<List<SysMenuDto>>(allMenus);

            //查询当前用户角色
            var userRoleIds = await _sysUserRepository.FindUserRoleAsync(sysUserDto.Id);
            if (userRoleIds == null)
                return result;

            //根据角色查询角色拥有的菜单ID
            List<Guid> menuIds = new List<Guid>();
            foreach (var roleId in userRoleIds)
            {
                menuIds = menuIds.Union(await _sysRoleRepository.GetMenuListByRoleAsync(roleId)).ToList();
            }
            allMenus = allMenus.Where(it => menuIds.Contains(it.Id)).OrderBy(it => it.SerialNumber).ToList();
            return Mapper.Map<List<SysMenuDto>>(allMenus);
        }



    }
}
