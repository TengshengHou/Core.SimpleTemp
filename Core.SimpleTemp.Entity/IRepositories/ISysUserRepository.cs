using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Domain.IRepositories
{
    public interface ISysUserRepository : IRepository<SysUser>
    {
        Task<SysUser> FindUserForLoginAsync(string userName, string Pwd);

        Task<List<int>> FindUserRole(int userId);
    }
}
