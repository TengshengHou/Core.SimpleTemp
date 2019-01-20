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
    public class ScriptDetailsAppService : BaseAppService<ScriptDetailsDto, ScriptDetails, IScriptDetailsRepository>, IScriptDetailsAppService
    {
        public ScriptDetailsAppService(IScriptDetailsRepository repository) : base(repository)
        {
        }

    }
}
