using Core.SimpleTemp.Application.UserApp;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application.ServiceApp.SysApp.LoginLogApp
{
    [AutoDi(typeof(SysLoginLogAppService))]
    public interface ISysLoginLogAppService : IBaseAppService<SysLoginLogDto, Core.SimpleTemp.Entitys.Sys.SysLoginLog>
    {
        Task<SysLoginLogDto> InsertLogAsync(SysLoginLogDto userDto);
    }
}
