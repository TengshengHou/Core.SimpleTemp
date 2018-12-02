using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.RepositoryEntityFrameworkCore.Demo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Repositories.IRepositories.Demo
{
    [AutoDi(typeof(RoleMenuRepository))]
    public interface IRoleMenuRepository : IRepository<SysRoleMenu>
    {
    }
}
