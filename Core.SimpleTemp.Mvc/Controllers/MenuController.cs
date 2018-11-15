using Core.SimpleTemp.Mvc.Models;
using Core.SimpleTemp.Service.MenuApp;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Mvc.Controllers
{
    [Route("Menu")]
    public class MenuController : Controller
    {
        private readonly ISysMenuAppService _sysMenuAppService;
        public MenuController(ISysMenuAppService sysMenuAppService)
        {
            _sysMenuAppService = sysMenuAppService;
        }

        // GET: /<controller>/
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取功能树JSON数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenuTreeData")]
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
        public async Task<IActionResult> EditAsync(SysMenuDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = GetModelStateError()
                });
            }
            var model = await _sysMenuAppService.GetAsync(dto.Id);
            if (model == null)
            {
                await _sysMenuAppService.InsertAsync(dto);
                return Json(new { Result = "Success" });
            }
            else
            {
                await _sysMenuAppService.UpdateAsync(dto);
                return Json(new { Result = "Success" });
            }
            return Json(new { Result = "Faild" });
        }

        [HttpPost("DeleteMuti")]
        public async Task<IActionResult> DeleteMutiAsync(string ids)
        {
            try
            {
                string[] idArray = ids.Split(',');
                List<Guid> delIds = new List<Guid>();
                foreach (string id in idArray)
                {
                    delIds.Add(Guid.Parse(id));
                }
                await _sysMenuAppService.DeleteBatchAsync(delIds);
                return Json(new
                {
                    Result = "Success"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = ex.Message
                });
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _sysMenuAppService.DeleteAsync(id);
                return Json(new
                {
                    Result = "Success"
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = ex.Message
                });
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult> GetAsync(Guid id)
        {
            var dto = await _sysMenuAppService.GetAsync(id);
            return Json(dto);
        }


        public string GetModelStateError()
        {
            foreach (var item in ModelState.Values)
            {
                if (item.Errors.Count > 0)
                {
                    return item.Errors[0].ErrorMessage;
                }
            }
            return "";
        }
    }
}
