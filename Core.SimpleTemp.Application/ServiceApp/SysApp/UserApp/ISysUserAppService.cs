using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using System;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application.UserApp
{
    [AutoDi(typeof(SysUserAppService))]
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
