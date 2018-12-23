using AutoMapper;
using Core.SimpleTemp.Application.UserApp;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application.ServiceApp.SysApp.LoginLogApp
{

    public class SysLoginLogAppService : BaseAppService<SysLoginLogDto, Core.SimpleTemp.Entitys.Sys.SysLoginLog, ISysLoginLogRepository>, ISysLoginLogAppService
    {
        private IHttpContextAccessor _accessor;

        public SysLoginLogAppService(ISysLoginLogRepository repository, IHttpContextAccessor accessor) : base(repository)
        {
            _repository = repository;
            _accessor = accessor;
        }
        public Task<SysLoginLogDto> InsertLogAsync(SysLoginLogDto dto)
        {
            dto.Ip = _accessor.HttpContext.Request.GetIp();
            dto.OtherInfo = _accessor.HttpContext.Request.Headers["User-Agent"];
            dto.LoginLogTime = DateTime.Now;
            return base.InsertAsync(dto);

        }
    }
}
