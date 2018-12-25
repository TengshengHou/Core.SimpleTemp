using Core.SimpleTemp.Application;
using Core.SimpleTemp.Application.Authorization;
using Core.SimpleTemp.Application.UserApp;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Core.SimpleTemp.Mvc.Controllers
{
    [Authorize]
    [Route("Department")]
    public class DepartmentController : AjaxController<SysDepartmentDto, SysDepartment, ISysDepartmentAppService>
    {
        private readonly ISysDepartmentAppService _service;
        private readonly ISysUserAppService _sysUserAppService;
        public DepartmentController(ISysDepartmentAppService service, ISysUserAppService sysUserAppService) : base(service)
        {
            _service = service;
            _sysUserAppService = sysUserAppService;
        }

        [HttpGet("index")]
        [PermissionFilter(DepartmentPermission.Department_Index)]
        public async Task<IActionResult> IndexAsync()
        {
            await AuthorizeListAsync(new string[] { DepartmentPermission.Department_Delete, DepartmentPermission.Department_Edit, DepartmentPermission.Department_DeleteMuti, DepartmentPermission.Department_details });
            return base.Index();
        }

        /// <summary>
        /// 新增或编辑功能
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        [PermissionFilter(DepartmentPermission.Department_Edit)]
        public async Task<IActionResult> SaveAsync(SysDepartmentDto dto)
        {
            return await base.SaveAsync(dto);
        }

        [HttpGet("Edit")]
        [PermissionFilter(DepartmentPermission.Department_Edit)]

        public async Task<IActionResult> EditAsync(Guid id, Guid ParentId)
        {
            SysDepartmentDto model = new SysDepartmentDto();
            if (id != Guid.Empty)
            {
                model = await _service.GetAsync(id);
            }
            else
            {
                model.ParentId = ParentId;
            }
            return View("Edit", model);
        }

        [HttpGet("details")]
        [PermissionFilter(DepartmentPermission.Department_details)]

        public async Task<IActionResult> Details(Guid id)
        {
            return await this.EditAsync(id, Guid.Empty);
        }

        [HttpPost("DeleteMuti")]
        [PermissionFilter(DepartmentPermission.Department_DeleteMuti)]
        public override async Task<IActionResult> DeleteMutiAsync(string ids)
        {
            Guid[] idArray = base.Str2GuidArray(ids);
            //有子节点不能删除
            var retbool = await _service.IsNoneChildren(idArray);
            if (!retbool)
            {
                return JsonFaild("删除失败,不能删除带有子节点的数据");
            }
            var user = await _sysUserAppService.FirstOrDefaultAsync(u => idArray.Contains(u.SysDepartmentId));
            if (!object.Equals(user, null))
            {
                return JsonFaild("删除失败,此部门下还存在用户数据");
            }
            return await base.DeleteMutiAsync(ids);
        }

        [HttpPost("Delete")]
        [PermissionFilter(DepartmentPermission.Department_Delete)]
        public override async Task<IActionResult> DeleteAsync(Guid id)
        {
            //有子节点不能删除
            var retbool = await _service.IsNoneChildren(new Guid[] { id });
            if (!retbool)
            {
                return JsonFaild("删除失败,不能删除带有子节点的数据");
            }
            var user = await _sysUserAppService.FirstOrDefaultAsync(u => u.SysDepartmentId == id);
            if (!object.Equals(user, null))
            {
                return JsonFaild("删除失败,此部门下还存在用户数据");
            }
            return await base.DeleteAsync(id);
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
        public async Task<IActionResult> GetChildrenByParentAsync()
        {
            var pagingQueryModel = base.GetPagingQueryModel();
            var result = await _service.LoadPageOffsetAsync(pagingQueryModel.Offset, pagingQueryModel.Limit, pagingQueryModel.FilterExpression, orderModel => orderModel.CreateTime);
            return JsonSuccess(result);
        }


        [HttpGet("SelcetTwoAsync")]
        public async Task<JsonResult> SelcetTwoAsync(string q, int page, int pageSize)
        {
            var trueExp = ExpressionExtension.True<SysDepartment>();
            if (!Equals(q, null))
                trueExp = trueExp.And(d => d.Name.Contains(q));
            var result = await _service.GetAllPageListAsync(page, pageSize, trueExp, d => d.Name);
            return JsonSuccess(result);
        }

    }
}

