﻿using Core.SimpleTemp.Application.Authorization;
using Core.SimpleTemp.Application.RoleApp;
using Core.SimpleTemp.Application.UserApp;
using Core.SimpleTemp.Entitys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.SimpleTemp.Mvc.Controllers.Internal;
namespace Core.SimpleTemp.Mvc.Controllers
{
    [Authorize]
    [Route("Role")]
    public class RoleController : AjaxController<SysRoleDto, SysRole, ISysRoleAppService>
    {
        private readonly ISysRoleAppService _service;
        private readonly ISysUserAppService _sysUserAppService;
        public RoleController(ISysRoleAppService service, ISysUserAppService sysUserAppService) : base(service)
        {
            _service = service;
            _sysUserAppService = sysUserAppService;
        }

        // GET: /<controller>/
        [HttpGet("Index")]
        [PermissionFilter(RolePermission.Role_Index)]
        public async Task<IActionResult> IndexAsync()
        {
            await AuthorizeListAsync(new string[] { RolePermission.Role_Edit, RolePermission.Role_Delete, RolePermission.Role_DeleteMuti, RolePermission.Role_details, RolePermission.Role_SavePermission });
            return base.Index();
        }

        /// <summary>
        /// 新增或编辑功能
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        [PermissionFilter(RolePermission.Role_Edit)]
        public async Task<IActionResult> SaveAsync(SysRoleDto dto)
        {
            return await base.SaveAsync(dto);
        }

        [HttpGet("Edit")]
        [PermissionFilter(RolePermission.Role_Edit)]

        public async Task<IActionResult> EditAsync(Guid id)
        {
            SysRoleDto model = new SysRoleDto();
            if (id != Guid.Empty)
            {
                model = await _service.GetAsync(id);
            }
            return View("Edit", model);
        }

        [HttpGet("details")]
        [PermissionFilter(RolePermission.Role_details)]

        public async Task<IActionResult> Details(Guid id)
        {
            return await this.EditAsync(id);
        }


        [HttpPost("DeleteMuti")]
        [PermissionFilter(RolePermission.Role_DeleteMuti)]
        public override async Task<IActionResult> DeleteMutiAsync(string ids)
        {
            var user = await _sysUserAppService.FindFirstUserRoleByRoleIdsAsync(base.Str2GuidArray(ids));
            if (!object.Equals(user, null))
            {
                return this.JsonFaild("删除失败,此角色下还存在用户数据");
            }
            return await base.DeleteMutiAsync(ids);
        }

        [HttpPost("Delete")]
        [PermissionFilter(RolePermission.Role_Delete)]
        public override async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = await _sysUserAppService.FindFirstUserRoleByRoleIdsAsync(new Guid[1] { id });
            if (!object.Equals(user, null))
            {
                return this.JsonFaild("删除失败,此角色下还存在用户数据");
            }
            return await base.DeleteAsync(id);
        }

        [HttpGet("Get")]
        [PermissionFilter(RolePermission.Role_Get)]
        public override async Task<IActionResult> GetAsync(Guid id)
        {
            return await base.GetAsync(id);
        }


        [HttpPost("GetAllPageList")]
        [PermissionFilter(RolePermission.Role_GetAllPageList)]
        public async Task<IActionResult> GetAllPageListAsync()
        {
            var pagingQueryModel = base.GetPagingQueryModel();
            var result = await _service.LoadPageOffsetAsync(pagingQueryModel.Offset, pagingQueryModel.Limit, pagingQueryModel.FilterExpression, orderModel => orderModel.CreateTime);
            return this.JsonSuccess(result);
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
                return this.JsonSuccess();
            }
            return this.JsonFaild();
        }
    }
}
