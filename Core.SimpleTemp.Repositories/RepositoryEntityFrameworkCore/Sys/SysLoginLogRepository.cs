﻿using Core.SimpleTemp.Entitys;
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
    public class SysLoginLogRepository : BaseRepository<SysLoginLog>, ISysLoginLogRepository
    {
        public SysLoginLogRepository(CoreDBContext dbContext) : base(dbContext)
        {
        }
    }
}
