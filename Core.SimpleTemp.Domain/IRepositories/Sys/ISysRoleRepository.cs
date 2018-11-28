using Core.SimpleTemp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Domain.IRepositories
{

    public interface ISysRoleRepository : IRepository<SysRole>
    {
        Task<List<Guid>> GetMenuListByRoleAsync(Guid roleId);
        Task<bool> UpdateRoleMenuAsync(Guid sysRoleId, List<SysRoleMenu> sysRoleMenus);
    }
}
