using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal;

namespace Core.SimpleTemp.Repositories.RepositoryEntityFrameworkCore.Sys
{
    public class SysDepartmentRepository : BaseRepository<SysDepartment>, ISysDepartmentRepository
    {
        public SysDepartmentRepository(CoreDBContext dbContext) : base(dbContext)
        {
        }
    }

}
