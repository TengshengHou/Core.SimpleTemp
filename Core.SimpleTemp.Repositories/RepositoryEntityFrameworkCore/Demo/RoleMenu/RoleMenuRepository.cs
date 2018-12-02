using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories.Demo;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Repositories.RepositoryEntityFrameworkCore.Demo
{
    public class RoleMenuRepository : BaseRepository<SysRoleMenu>, IRoleMenuRepository
    {
        public RoleMenuRepository(CoreDBContext dbContext) : base(dbContext)
        {
        }

    }
}
