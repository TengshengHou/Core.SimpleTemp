using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Entitys.Script;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal;

namespace Core.SimpleTemp.Repositories.RepositoryEntityFrameworkCore
{
    public class ScriptRepository : BaseRepository<BusinessDBContext, Script>, IScriptRepository
    {
        public ScriptRepository(BusinessDBContext dbContext) : base(dbContext)
        {
        }
    }

}
