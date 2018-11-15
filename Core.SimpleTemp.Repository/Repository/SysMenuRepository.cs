using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Repository
{
    public class SysMenuRepository : BaseRepository<SysMenu>, ISysMenuRepository
    {
        public SysMenuRepository(CoreDBContext dbContext) : base(dbContext)
        {
        }
    }
}
