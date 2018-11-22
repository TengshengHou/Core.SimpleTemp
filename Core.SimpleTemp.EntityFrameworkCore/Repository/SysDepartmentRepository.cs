using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Repository.Repository
{
    public class SysDepartmentRepository : BaseRepository<SysDepartment>, ISysDepartmentRepository
    {
        public SysDepartmentRepository(CoreDBContext dbContext) : base(dbContext)
        {
        }
    }

}
