using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories.Internal.Data;
using System;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Service.UserApp
{
    public interface ISysUserAppService : IBaseAppService<SysUserDto, SysUser>
    {

        Task<IPageModel<SysUserDto>> GetUserByDepartmentAsync(Guid departmentId, int startPage, int pageSize);

        Task<bool> UpdatePwdAsync(UpdatePwdDto dto);

        /// <summary>
        /// 根据用户名重置密码
        /// </summary>
        /// <param name="LoginName"></param>
        /// <returns></returns>
        Task RestoreUserPwdAsync(string LoginName);
    }
}
