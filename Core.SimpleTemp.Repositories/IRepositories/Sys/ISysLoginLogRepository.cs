using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Entitys.Sys;
using Core.SimpleTemp.Repositories.RepositoryEntityFrameworkCore.Sys;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Repositories.IRepositories
{

    [AutoDi(typeof(SysLoginLogRepository))]
    public interface ISysLoginLogRepository : IRepository<SysLoginLog>
    {
     
    }
}
