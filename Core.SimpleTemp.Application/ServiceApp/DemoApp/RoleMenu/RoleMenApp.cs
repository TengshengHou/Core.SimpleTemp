using Core.SimpleTemp.Application.RoleApp;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories.Demo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.SimpleTemp.Application.ServiceApp.DemoApp.RoleMenu
{
    [AutoDi(typeof(RoleMenApp))]
    public interface IRoleMenApp : IBaseAppService<RoleMenuDto, SysRoleMenu>
    {
        System.Threading.Tasks.Task<List<RoleMenuDto>> GetListAsync();
    }
    public class RoleMenApp : BaseAppService<RoleMenuDto, SysRoleMenu, IRoleMenuRepository>, IRoleMenApp
    {
        public RoleMenApp(IRoleMenuRepository roleMenuRepository) : base(roleMenuRepository)
        {
        }
        public async System.Threading.Tasks.Task<List<RoleMenuDto>> GetListAsync()
        {

            var list = await _repository.GetAllListAsync();
            var ret = AutoMapper.Mapper.Map<List<RoleMenuDto>>(list);
            return ret;
        }
    }
}
