using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Entitys.Sys;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Repositories.RepositoryEntityFrameworkCore.Sys
{
    /// <summary>
    /// 登录日志记录仓储
    /// *此类为演示支持多DB
    /// </summary>
    public class SysLoginLogRepository : BaseRepository<LogDBContext,SysLoginLog>, ISysLoginLogRepository
    {
        public SysLoginLogRepository(LogDBContext dbContext) : base(dbContext)
        {
        }
    }
}
