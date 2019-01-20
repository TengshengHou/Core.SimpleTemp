using AutoMapper;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Core.SimpleTemp.Entitys.Script;
using Core.SimpleTemp.Application.ServiceApp.ScriptApp;
using Core.SimpleTemp.Repositories.RepositoryEntityFrameworkCore;

namespace Core.SimpleTemp.Application
{
    public class ScriptAppService : BaseAppService<ScriptDto, Script, IScriptRepository>, IScriptAppService
    {
        public ScriptAppService(IScriptRepository repository) : base(repository)
        {
        }

    }
}
