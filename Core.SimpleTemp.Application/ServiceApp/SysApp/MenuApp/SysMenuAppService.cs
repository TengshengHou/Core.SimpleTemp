using AutoMapper;
using Core.SimpleTemp.Application.UserApp;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal.Data;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application.MenuApp
{

    public class SysMenuAppService : BaseAppService<SysMenuDto, SysMenu, ISysMenuRepository>, ISysMenuAppService
    {
        private readonly ISysMenuRepository _sysMenuRepository;
        private readonly ISysUserRepository _sysUserRepository;
        private readonly ISysRoleRepository _sysRoleRepository;
        private readonly IDistributedCache _distributedCache;

        public SysMenuAppService(ISysMenuRepository sysMenuRepository, ISysUserRepository sysUserRepository, ISysRoleRepository sysRoleRepository, IDistributedCache distributedCache) : base(sysMenuRepository)
        {
            _sysMenuRepository = sysMenuRepository;
            _sysUserRepository = sysUserRepository;
            _sysRoleRepository = sysRoleRepository;
            _distributedCache = distributedCache;
        }


        public async Task<IPageModel<SysMenuDto>> GetMenusByParentAsync(Guid parentId, int startPage, int pageSize)
        {
            var pageMenu = await GetAllPageListAsync(startPage, pageSize, it => it.ParentId == parentId, it => it.SerialNumber);
            IPageModel<SysMenuDto> pageModel = new PageModel<SysMenuDto>()
            {
                PageData = Mapper.Map<List<SysMenuDto>>(pageMenu.PageData),
                RowCount = pageMenu.RowCount
            };
            return pageModel;
        }

        /// <summary>
        /// 根据用户获取功能菜单并缓存
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public async Task<List<SysMenuDto>> GetMenusAndFunctionByUserAsync(SysUserDto sysUserDto)
        {
            List<SysMenuDto> ret = new List<SysMenuDto>();
            JsonSerializer jsonSerializer = new JsonSerializer();
            var checheKey = SysConsts.MENU_CACHEKEY_PREFIX + sysUserDto.Id;
            //缓存
            if ((await _distributedCache.GetAsync(checheKey)) == null)
            {
                ret = await this.GetMenusAsync(sysUserDto);
                //序列化好麻烦
                var stringWriter = new StringWriter();
                var jsonWriter = new JsonTextWriter(stringWriter);
                jsonSerializer.Serialize(jsonWriter, ret);

                await _distributedCache.SetStringAsync(checheKey, stringWriter.ToString(), new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(5) });
            }
            else
            {
                var jsonString = await _distributedCache.GetStringAsync(checheKey);
                var stringReader = new StringReader(jsonString);
                var jsonTextReader = new JsonTextReader(stringReader);

                ret = jsonSerializer.Deserialize<List<SysMenuDto>>(jsonTextReader);
            }

            return ret;
        }

        /// <summary>
        /// 根据用户获取功能菜单
        /// </summary>
        /// <param name="sysUserDto"></param>
        /// <returns></returns>
        private async Task<List<SysMenuDto>> GetMenusAsync(SysUserDto sysUserDto)
        {
            //查询出系统所有菜单
            List<SysMenuDto> result = new List<SysMenuDto>();
            var allMenus = await _sysMenuRepository.GetAllListAsync();
            allMenus = allMenus.OrderBy(it => it.SerialNumber).ToList();

            if (sysUserDto.LoginName == WebAppConfiguration.AdminLoginName) //超级管理员
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
