using Core.SimpleTemp.Application.ServiceApp.SysApp.FileApp;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Entitys.Sys;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application.UserApp
{
    [AutoDi(typeof(SysFileAppService))]
    public interface ISysFileAppService : IBaseAppService<SysFileDto, SysFile>
    {
        Task<List<SysFileDto>> UploadFileAsync(UpdateFileDto updateFileDto);
    }
}
