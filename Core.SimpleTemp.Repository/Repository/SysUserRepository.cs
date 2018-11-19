using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Repository
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
            var user = await FirstOrDefaultAsync(u => userName.Equals(userName) && u.Password.Equals(Pwd));

            return user;
        }

        public override Task<SysUser> GetAsync(Guid id)
        {
            return _dbContext.Set<SysUser>().Include(u => u.UserRoles).FirstOrDefaultAsync(CreateEqualityExpressionForId(id));
        }

        public Task<List<Guid>> FindUserRoleAsync(Guid userId)
        {
            return _dbContext.SysUserRole.Where(ur => ur.SysUserId == userId).AsNoTracking().Select(ur => ur.SysRoleId).ToListAsync();
        }

        //public override async Task<SysUser> UpdateAsync(SysUser entity, bool autoSave = true, List<string> noUpdateProperties = null)
        //{
        //    var remList = _dbContext.SysUserRole.Where(UR => UR.SysUserId == entity.Id);
        //    //remList.ForEachAsync(ur=> )
        //    _dbContext.SysUserRole.RemoveRange(remList);
        //    await _dbContext.SaveChangesAsync();
        //    return await base.UpdateAsync(entity, autoSave, noUpdateProperties);
        //}
    }
}
