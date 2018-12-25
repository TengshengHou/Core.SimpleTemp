using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Repositories.RepositoryEntityFrameworkCore.Sys
{
    public class SysUserRepository : BaseRepository<SysUser>, ISysUserRepository
    {
        public SysUserRepository(CoreDBContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// 根据用户名密码查找用户，用于用户登录逻辑
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>

        public async Task<SysUser> FindUserForLoginAsync(string userName, string Pwd)
        {
            var user = await FirstOrDefaultAsync(u => u.LoginName.Equals(userName) && u.Password.Equals(Pwd));

            return user;
        }

        public override IQueryable<SysUser> QueryBase()
        {
            return _dbContext.SysUser.Include(u => u.UserRoles);
        }

        public Task<List<Guid>> FindUserRoleAsync(Guid userId)
        {
            return _dbContext.SysUserRole.Where(ur => ur.SysUserId == userId).AsNoTracking().Select(ur => ur.SysRoleId).ToListAsync();
        }

        public async Task<SysUserRole> FindFirstUserRoleByRoleIdsAsync(Guid[] roleIds)
        {
            return await _dbContext.SysUserRole.Include(ur => ur.SysUser).FirstOrDefaultAsync(ur => roleIds.Contains(ur.SysRoleId));
        }

    }
}
