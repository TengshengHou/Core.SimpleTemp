using Core.SimpleTemp.Domain.Entities;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Repository
{
    public interface ISysUserRepository
    {
        Task<SysUser> FindUserForLoginAsync(string userName, string Pwd);
    }
}
