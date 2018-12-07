using AutoMapper;
using Core.SimpleTemp.Application.RoleApp;
using Core.SimpleTemp.Application.ServiceApp.DemoApp.RoleMenu;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Common.FilterExpression;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Core.SimpleTemp.Mvc.Controllers.Demo.RoleMenu
{

    [Route("RoleMenu")]
    public class RoleMenuController : AjaxController<RoleMenuDto, SysRoleMenu, IRoleMenApp>
    {
        IRoleMenApp _roleMenApp;
        ISysRoleAppService _sysRoleAppService;
        public RoleMenuController(IRoleMenApp roleMenApp, ISysRoleAppService sysRoleAppService) : base(roleMenApp)
        {
            _roleMenApp = roleMenApp;
            _sysRoleAppService = sysRoleAppService;
        }



        [HttpPost("GetList")]
        public async Task<IActionResult> GetListAsync(int offset, int limit, RoleMenuDto dto)
        {
            var pagingQueryModel = GetPagingQueryModel();
            var v = await _roleMenApp.ILoadPageOffsetAsync(pagingQueryModel.Offset, pagingQueryModel.Limit, new string[] { nameof(SysRoleMenu.SysMenu), nameof(SysRoleMenu.SysRole) }, pagingQueryModel.FilterExpression, a => a.Id);
            return JsonSuccess(v);
        }

        [HttpGet("index")]
        public async Task<IActionResult> IndexAsync()
        {
            var roleList = await _sysRoleAppService.GetAllListAsync();
            var roleSelectList = roleList.Select(role => (new SelectListItem() { Text = role.Name, Value = role.Id.ToString() }));
            ViewBag.roleSelectList = roleSelectList;
            return View("index");
        }


        public async Task LamdTestAsync()
        {

            RoleMenuDto roleMenuDto = new RoleMenuDto();
            roleMenuDto = await _roleMenApp.FirstOrDefaultAsync(r => r.SysRole.Name == "角色管理员");
            //Id	Code	Name	CreateUserId	CreateTime	Remarks
            //885C4941-BFE -4872-F55F-08D6581C4640 角色管理员   角色管理员   0   NULL 角色管理员

            List<FilterModel> list = new List<FilterModel>();
            var filter1 = new FilterModel() { Field = "SysMenu.Id", Value = roleMenuDto.SysMenuId, DataType = "string", Logic = "AND", Action = "=" };
            list.Add(filter1);
            var filterlist = ExpressionExtension.GetFilterExpression<SysRoleMenu>(list);

            var entity = Mapper.Map<SysRoleMenu>(roleMenuDto);
            var roleMenuDto2 = await _roleMenApp.IFirstOrDefaultAsync(filterlist, new string[] { nameof(SysRoleMenu.SysMenu), nameof(SysRoleMenu.SysRole) });
        }
    }
}