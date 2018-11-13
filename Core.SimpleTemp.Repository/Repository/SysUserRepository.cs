using Core.SimpleTemp.Domain.Entities;
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

        public Task<SysUser> FindUserForLoginAsync(string userName, string Pwd)
        {
            return FirstOrDefaultAsync(u => userName.Equals(userName) && u.Password.Equals(Pwd));

        }


    }
}
