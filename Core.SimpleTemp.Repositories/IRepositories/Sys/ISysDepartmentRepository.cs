using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.RepositoryEntityFrameworkCore.Sys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Repositories.IRepositories
{
    [AutoDi(typeof(SysDepartmentRepository))]
    public interface ISysDepartmentRepository : IRepository<SysDepartment>
    {
    }
}
