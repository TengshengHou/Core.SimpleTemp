using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.RepositoryEntityFrameworkCore.Sys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Repositories.IRepositories
{

    [AutoDi(typeof(SysUserRepository))]
    public interface ISysUserRepository : IRepository<SysUser>
    {
        Task<SysUser> FindUserForLoginAsync(string userName, string Pwd);

        Task<List<Guid>> FindUserRoleAsync(Guid userId);
    }
}
