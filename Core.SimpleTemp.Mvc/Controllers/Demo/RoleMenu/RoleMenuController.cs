using Core.SimpleTemp.Application.ServiceApp.DemoApp.RoleMenu;
using Core.SimpleTemp.Entitys;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
namespace Core.SimpleTemp.Mvc.Controllers.Demo.RoleMenu
{

    [Route("RoleMenu")]
    public class RoleMenuController : AjaxController<RoleMenuDto, SysRoleMenu, IRoleMenApp>
    {
        IRoleMenApp _roleMenApp;
        public RoleMenuController(IRoleMenApp roleMenApp) : base(roleMenApp)
        {
            _roleMenApp = roleMenApp;
        }
        [HttpGet("index")]
        public  async Task<IActionResult> IndexAsync()
        {
            var v = await _roleMenApp.GetListAsync();
            var ret = from r in v select new { r.SysMenu, r.SysRole };
            return JsonSuccess(ret);
        }
    }
}