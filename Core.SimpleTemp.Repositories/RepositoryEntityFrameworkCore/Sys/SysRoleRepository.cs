﻿using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Core.SimpleTemp.Repositories.RepositoryEntityFrameworkCore.Sys
{

    public class SysRoleRepository : BaseRepository<SysRole>, ISysRoleRepository
    {
        public SysRoleRepository(CoreDBContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <returns></returns>
        public Task<List<Guid>> GetMenuListByRoleAsync(Guid roleId)
        {
            var roleMenus = _dbContext.Set<SysRoleMenu>().Where(rm => rm.SysRoleId == roleId).AsNoTracking();
            var sysMenuIds = from t in roleMenus select t.SysMenuId;
            return sysMenuIds.ToListAsync();
        }

        /// <summary>
        /// 更新角色权限关联关系
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="roleMenus">角色权限集合</param>
        /// <returns></returns>
        public async Task<bool> UpdateRoleMenuAsync(Guid sysRoleId, List<SysRoleMenu> sysRoleMenus)
        {
            var oldDatas = await _dbContext.Set<SysRoleMenu>().Where(it => it.SysRoleId == sysRoleId).ToListAsync();
            oldDatas.ForEach(it => _dbContext.Set<SysRoleMenu>().Remove(it));
            await _dbContext.SaveChangesAsync();
            _dbContext.Set<SysRoleMenu>().AddRange(sysRoleMenus);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
