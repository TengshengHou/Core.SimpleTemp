using Core.SimpleTemp.Application.Authorization;
using Core.SimpleTemp.Application.MenuApp;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.SimpleTemp.Mvc.Controllers.Internal;
namespace Core.SimpleTemp.Mvc.Controllers
{
    [Authorize]
    [Route("Menu")]
    public class MenuController : AjaxController<SysMenuDto, SysMenu, ISysMenuAppService>
    {
        private readonly ISysMenuAppService _sysMenuAppService;
        public MenuController(ISysMenuAppService sysMenuAppService) : base(sysMenuAppService)
        {
            _sysMenuAppService = sysMenuAppService;
        }

        // GET: /<controller>/
        [HttpGet("index")]

        [PermissionFilter(MenuPermission.Menu_Index)]
        public async Task<IActionResult> IndexAsync()
        {
            await AuthorizeListAsync(new string[] { MenuPermission.Menu_Delete, MenuPermission.Menu_Edit, MenuPermission.Menu_DeleteMuti });
            return base.Index();
        }
        /// <summary>
        /// 获取功能树JSON数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenuTreeData")]
        [PermissionFilter(MenuPermission.Menu_GetMenuTreeData)]
        public async Task<IActionResult> GetMenuTreeDataAsync()
        {
            var menus = await _sysMenuAppService.GetAllListAsync();
            List<TreeModel> treeModels = new List<TreeModel>();
            foreach (var menu in menus)
            {
                treeModels.Add(new TreeModel() { Id = menu.Id.ToString(), Text = menu.Name, Parent = menu.ParentId == Guid.Empty ? "#" : menu.ParentId.ToString() });
            }
            return Json(treeModels);
        }

        /// <summary>
        /// 获取子级功能列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMneusByParent")]
        [PermissionFilter(MenuPermission.Menu_GetMneusByParent)]
        public async Task<IActionResult> GetMneusByParentAsync(Guid parentId, int startPage, int pageSize)
        {
            int rowCount = 0;
            var pageModel = await _sysMenuAppService.GetMenusByParentAsync(parentId, startPage, pageSize);
            rowCount = pageModel.RowCount;

            return Json(new
            {
                rowCount = rowCount,
                pageCount = Math.Ceiling(Convert.ToDecimal(rowCount) / pageSize),
                rows = pageModel.PageData,
            });
        }

        /// <summary>
        /// 新增或编辑功能
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        [PermissionFilter(MenuPermission.Menu_Edit)]
        public async Task<IActionResult> SaveAsync(SysMenuDto dto)
        {
            return await base.SaveAsync(dto);
        }

        [HttpPost("DeleteMuti")]
        [PermissionFilter(MenuPermission.Menu_DeleteMuti)]
        public override async Task<IActionResult> DeleteMutiAsync(string ids)
        {

            var retbool = await _sysMenuAppService.IsNoneChildren(base.Str2GuidArray(ids));
            //有子节点不能删除
            if (!retbool)
            {
                return this.JsonFaild("删除失败,不能删除带有子节点的数据");
            }
            return await base.DeleteMutiAsync(ids);
        }

        [HttpPost("Delete")]
        [PermissionFilter(MenuPermission.Menu_Delete)]
        public override async Task<IActionResult> DeleteAsync(Guid id)
        {

            var retbool = await _sysMenuAppService.IsNoneChildren(new Guid[1] { id });
            //有子节点不能删除
            if (!retbool)
            {
                return this.JsonFaild("删除失败,不能删除带有子节点的数据");
            }
            return await base.DeleteAsync(id);

        }

        [HttpGet("Get")]
        [PermissionFilter(MenuPermission.Menu_Get)]
        public override async Task<IActionResult> GetAsync(Guid id)
        {
            return await base.GetAsync(id);
        }

    }
}
