using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys.Script;
using Core.SimpleTemp.Repositories.RepositoryEntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Repositories.IRepositories
{
    [AutoDi(typeof(ScriptDetailsRepository))]
    public interface IScriptDetailsRepository : IRepository<ScriptDetails>
    {
    }
}
