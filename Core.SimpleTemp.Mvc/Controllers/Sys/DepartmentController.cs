using Core.SimpleTemp.Application;
using Core.SimpleTemp.Application.Authorization;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Core.SimpleTemp.Mvc.Controllers
{
    [Authorize]
    [Route("Department")]
    public class DepartmentController : AjaxController<SysDepartmentDto, SysDepartment, ISysDepartmentAppService>
    {
        private readonly ISysDepartmentAppService _service;
        public DepartmentController(ISysDepartmentAppService service) : base(service)
        {
            _service = service;
        }

        [HttpGet("index")]
        [PermissionFilter(DepartmentPermission.Department_Index)]
        public async Task<IActionResult> IndexAsync()
        {
            await AuthorizeListAsync(new string[] { DepartmentPermission.Department_Delete, DepartmentPermission.Department_Edit, DepartmentPermission.Department_DeleteMuti });
            return base.Index();
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
            return await base.EditAsync(dto);
        }

        [HttpPost("DeleteMuti")]
        [PermissionFilter(DepartmentPermission.Department_DeleteMuti)]
        public override async Task<IActionResult> DeleteMutiAsync(string ids)
        {
            string[] idArray = ids.Split(',');
            List<Guid> delIds = new List<Guid>();
            foreach (string id in idArray)
            {
                delIds.Add(Guid.Parse(id));
            }
            var retbool = await _service.IsNoneChildren(delIds);
            //有子节点不能删除
            if (!retbool)
            {
                return Json(new
                {
                    Result = "Faild",

                    Message = "删除失败,不能删除带有子节点的数据"
                });
            }
            return await base.DeleteMutiAsync(ids);
        }

        [HttpPost("Delete")]
        [PermissionFilter(DepartmentPermission.Department_Delete)]
        public override async Task<IActionResult> DeleteAsync(Guid id)
        {

            var retbool = await _service.IsNoneChildren(new List<Guid>() { id });
            //有子节点不能删除
            if (!retbool)
            {
                return JsonFaild("删除失败,不能删除带有子节点的数据");
            }

            return await base.DeleteAsync(id);

        }
        [HttpGet("Get")]
        [PermissionFilter(DepartmentPermission.Department_Get)]
        public override async Task<IActionResult> GetAsync(Guid id)
        {
            return await base.GetAsync(id);
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
        [HttpPost("GetChildrenByParent")]
        [PermissionFilter(DepartmentPermission.Department_GetChildrenByParent)]
        public async Task<IActionResult> GetChildrenByParentAsync(Guid parentId)
        {
            var pagingQueryModel = base.GetPagingQueryModel();
            var result = await _service.LoadPageOffsetAsync(pagingQueryModel.Offset, pagingQueryModel.Limit, model => model.ParentId == parentId, orderModel => orderModel.CreateTime);
            return JsonSuccess(result);
        }









    }
}

