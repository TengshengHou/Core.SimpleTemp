using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.RepositoryEntityFrameworkCore.Sys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Repositories.IRepositories
{

    [AutoDi(typeof(SysRoleRepository))]
    public interface ISysRoleRepository : IRepository<SysRole>
    {
        Task<List<Guid>> GetMenuListByRoleAsync(Guid roleId);
        Task<bool> UpdateRoleMenuAsync(Guid sysRoleId, List<SysRoleMenu> sysRoleMenus);
    }
}
