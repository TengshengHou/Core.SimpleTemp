using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Entitys.Script;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal;

namespace Core.SimpleTemp.Repositories.RepositoryEntityFrameworkCore
{
    public class ScriptDetailsRepository : BaseRepository<BusinessDBContext, ScriptDetails>, IScriptDetailsRepository
    {
        public ScriptDetailsRepository(BusinessDBContext dbContext) : base(dbContext)
        {
        }
    }

}
