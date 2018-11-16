using Core.SimpleTemp.Service.MenuApp;
using Core.SimpleTemp.Service.RoleApp;
using Core.SimpleTemp.Service.RoleApp.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Mvc.Controllers
{
    [Route("Role")]
    public class RoleController : Controller
    {
        private readonly ISysRoleAppService _service;
        public RoleController(ISysRoleAppService service)
        {
            _service = service;
        }

        // GET: /<controller>/
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 新增或编辑功能
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IActionResult> EditAsync(SysRoleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    Result = "Faild",
                    Message = GetModelStateError()
                });
            }
            var model = await _service.GetAsync(dto.Id);
            if (model == null)
            {
                await _service.InsertAsync(dto);
                return Json(new { Result = "Success" });
            }
            else
            {
                await _service.UpdateAsync(dto);
                return Json(new { Result = "Success" });
            }
        }

        public async Task<IActionResult> GetAllPageListAsync(int startPage, int pageSize)
        {
            int rowCount = 0;
            var result = await _service.GetAllPageListAsync(startPage, pageSize);
            rowCount = result.RowCount;
            return Json(new
            {
                rowCount = rowCount,
                pageCount = Math.Ceiling(Convert.ToDecimal(rowCount) / pageSize),
                rows = result.PageData,
            });
        }
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
                await _service.DeleteBatchAsync(delIds);
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
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
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
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var dto = await _service.GetAsync(id);
            return Json(dto);
        }

        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetMenusByRoleAsync(Guid roleId)
        {
            var dtos = await _service.GetMenuListByRoleAsync(roleId);
            return Json(dtos);
        }

        public async Task<IActionResult> SavePermissionAsync(Guid roleId, List<SysRoleMenuDto> roleMenus)
        {
            if (await _service.UpdateRoleMenuAsync(roleId, roleMenus))
            {
                return Json(new { Result = "Success" });
            }
            return Json(new { Result = "Faild" });
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
