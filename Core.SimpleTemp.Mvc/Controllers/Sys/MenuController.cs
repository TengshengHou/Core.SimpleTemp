using Core.SimpleTemp.Application.Authorization;
using Core.SimpleTemp.Mvc.Models;
using Core.SimpleTemp.Application.Authorization;
using Core.SimpleTemp.Application.MenuApp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.SimpleTemp.Entitys;

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
        public  async Task<IActionResult> IndexAsync()
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
        [HttpPost("Edit")]
        [PermissionFilter(MenuPermission.Menu_Edit)]
        public async Task<IActionResult> EditAsync(SysMenuDto dto)
        {
            return await base.EditAsync(dto);
        }

        [HttpPost("DeleteMuti")]
        [PermissionFilter(MenuPermission.Menu_DeleteMuti)]
        public override async Task<IActionResult> DeleteMutiAsync(string ids)
        {
            string[] idArray = ids.Split(',');
            List<Guid> delIds = new List<Guid>();
            foreach (string id in idArray)
            {
                delIds.Add(Guid.Parse(id));
            }
            var retbool = await _sysMenuAppService.IsNoneChildren(delIds);
            //有子节点不能删除
            if (!retbool)
            {
                return JsonFaild("删除失败,不能删除带有子节点的数据");
            }
            await _sysMenuAppService.DeleteBatchAsync(delIds);

            return await base.DeleteMutiAsync(ids);
        }

        [HttpPost("Delete")]
        [PermissionFilter(MenuPermission.Menu_Delete)]
        public override async Task<IActionResult> DeleteAsync(Guid id)
        {

            var retbool = await _sysMenuAppService.IsNoneChildren(new List<Guid>() { id });
            //有子节点不能删除
            if (!retbool)
            {
                return JsonFaild("删除失败,不能删除带有子节点的数据");
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
