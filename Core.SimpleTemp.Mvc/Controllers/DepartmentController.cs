using Core.SimpleTemp.Domain.Authorization;
using Core.SimpleTemp.Mvc.Models;
using Core.SimpleTemp.Service;
using Core.SimpleTemp.Service.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Core.SimpleTemp.Mvc.Controllers
{
    [Authorize]
    [Route("Department")]
    public class DepartmentController : Controller
    {
        private readonly ISysDepartmentAppService _service;
        public DepartmentController(ISysDepartmentAppService service)
        {
            _service = service;
        }



        // GET: /<controller>/
        [HttpGet("index")]
        [PermissionFilter(DepartmentPermission.Department_Index)]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取功能树JSON数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTreeData")]
        [PermissionFilter(DepartmentPermission.Department_GetTreeData)]
        public async Task<IActionResult> GetTreeDataAsync()
        {
            var dtos = await _service.GetAllListAsync();
            List<TreeModel> treeModels = new List<TreeModel>();
            foreach (var dto in dtos)
            {
                treeModels.Add(new TreeModel() { Id = dto.Id.ToString(), Text = dto.Name, Parent = dto.ParentId == Guid.Empty ? "#" : dto.ParentId.ToString() });
            }
            return Json(treeModels);
        }

        /// <summary>
        /// 获取子级列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetChildrenByParent")]
        [PermissionFilter(DepartmentPermission.Department_GetChildrenByParent)]
        public async Task<IActionResult> GetChildrenByParentAsync(Guid parentId, int startPage, int pageSize)
        {
            int rowCount = 0;
            var result = await _service.GetChildrenByParentAsync(parentId, startPage, pageSize);
            rowCount = result.RowCount;
            return Json(new
            {
                rowCount = rowCount,
                pageCount = Math.Ceiling(Convert.ToDecimal(rowCount) / pageSize),
                rows = result.PageData,
            });
        }


        /// <summary>
        /// 新增或编辑功能
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Edit")]
        [PermissionFilter(DepartmentPermission.Department_Edit)]
        public async Task<IActionResult> EditAsync(SysDepartmentDto dto)
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

        [HttpPost("DeleteMuti")]
        [PermissionFilter(DepartmentPermission.Department_DeleteMuti)]
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
        [HttpPost("Delete")]
        [PermissionFilter(DepartmentPermission.Department_Delete)]
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
        [HttpGet("Get")]
        [PermissionFilter(DepartmentPermission.Department_Get)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var dto = await _service.GetAsync(id);
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

