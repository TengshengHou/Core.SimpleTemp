using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal;

namespace Core.SimpleTemp.Repositories.RepositoryEntityFrameworkCore.Sys
{

    public class SysMenuRepository : BaseRepository<SysMenu>, ISysMenuRepository
    {
        public SysMenuRepository(CoreDBContext dbContext) : base(dbContext)
        {
        }
    }
}
