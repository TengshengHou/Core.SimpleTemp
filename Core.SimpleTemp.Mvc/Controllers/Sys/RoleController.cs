using Core.SimpleTemp.Application.Authorization;
using Core.SimpleTemp.Application.RoleApp;
using Core.SimpleTemp.Entitys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Mvc.Controllers
{
    [Authorize]
    [Route("Role")]
    public class RoleController : AjaxController<SysRoleDto, SysRole, ISysRoleAppService>
    {
        private readonly ISysRoleAppService _service;
        public RoleController(ISysRoleAppService service) : base(service)
        {
            _service = service;
        }

        // GET: /<controller>/
        [HttpGet("Index")]
        [PermissionFilter(RolePermission.Role_Index)]
        public override IActionResult Index()
        {
            return base.Index();
        }

        /// <summary>
        /// 新增或编辑功能
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Edit")]
        [PermissionFilter(RolePermission.Role_Edit)]
        public override async Task<IActionResult> EditAsync(SysRoleDto dto)
        {

            return await base.EditAsync(dto);
        }

        [HttpPost("DeleteMuti")]
        [PermissionFilter(RolePermission.Role_DeleteMuti)]
        public override async Task<IActionResult> DeleteMutiAsync(string ids)
        {
            return await base.DeleteMutiAsync(ids);
        }

        [HttpPost("Delete")]
        [PermissionFilter(RolePermission.Role_Delete)]
        public override async Task<IActionResult> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync(id);
        }

        [HttpGet("Get")]
        [PermissionFilter(RolePermission.Role_Get)]
        public override async Task<IActionResult> GetAsync(Guid id)
        {
            return await base.GetAsync(id);
        }


        [HttpGet("GetAllPageList")]
        [PermissionFilter(RolePermission.Role_GetAllPageList)]
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

        /// <summary>
        /// 根据角色获取权限
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenusByRole")]
        [PermissionFilter(RolePermission.Role_GetMenusByRole)]
        public async Task<IActionResult> GetMenusByRoleAsync(Guid roleId)
        {
            var dtos = await _service.GetMenuListByRoleAsync(roleId);
            return Json(dtos);
        }

        [HttpPost("SavePermission")]
        [PermissionFilter(RolePermission.Role_SavePermission)]
        public async Task<IActionResult> SavePermissionAsync(Guid roleId, List<SysRoleMenuDto> roleMenus)
        {
            if (await _service.UpdateRoleMenuAsync(roleId, roleMenus))
            {
                return JsonSuccess();
            }
            return JsonFaild();
        }
    }
}
