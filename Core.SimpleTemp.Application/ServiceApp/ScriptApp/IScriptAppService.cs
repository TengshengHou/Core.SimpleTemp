using Core.SimpleTemp.Application.ServiceApp.ScriptApp;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Entitys.Script;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application
{
    [AutoDi(typeof(ScriptAppService))]
    public interface IScriptAppService : IBaseAppService<ScriptDto, Script>
    {

     
    }
}
