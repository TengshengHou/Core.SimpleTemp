using Core.SimpleTemp.Application.Authorization;
using Core.SimpleTemp.Application.RoleApp;
using Core.SimpleTemp.Application.UserApp;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Mvc.Controllers
{
    [Authorize]
    [Route("User")]
    public class UserController : AjaxController<SysUserDto, SysUser, ISysUserAppService>
    {
        private readonly ISysUserAppService _service;
        private readonly ISysRoleAppService _roleService;

        public UserController(ISysUserAppService service, ISysRoleAppService sysRoleAppService) : base(service)
        {
            _service = service;
            _roleService = sysRoleAppService;
        }

        [HttpGet("Index")]
        [PermissionFilter(UserPermission.UserController_Index)]
        public async Task<IActionResult> IndexAsync()
        {
            await SetroleSelectListAsync();
            await base.AuthorizeListAsync(new string[] { UserPermission.UserController_Edit, UserPermission.UserController_DeleteMuti, UserPermission.UserController_Delete });
            return base.Index();
        }

        [HttpPost("GetList")]
        [PermissionFilter(UserPermission.UserController_GetList)]
        public async Task<IActionResult> GetList(string[] roles)
        {
            #region 过滤条件
            var pagingQueryModel = base.GetPagingQueryModel();
            if (!object.Equals(roles, null) && roles.Any())
            {
                if (pagingQueryModel.FilterExpression == null)
                {
                    pagingQueryModel.FilterExpression = ExpressionExtension.True<SysUser>();
                }
                pagingQueryModel.FilterExpression = pagingQueryModel.FilterExpression.And(u => u.UserRoles.Where(ur => roles.Contains(ur.SysRoleId.ToString())).Select(ur => ur.SysUserId).Contains(u.Id));
            }
            #endregion

            var result = await _service.LoadPageOffsetAsync(pagingQueryModel.Offset, pagingQueryModel.Limit, pagingQueryModel.FilterExpression, orderModel => orderModel.LastUpdate);
            result.PageData.ForEach(u => u.Password = string.Empty);
            return JsonSuccess(result);
        }

        [HttpGet("Edit")]
        [PermissionFilter(UserPermission.UserController_Edit)]
        public async Task<IActionResult> EditAsync(Guid id)
        {
            SysUserDto model = new SysUserDto();
            await SetroleSelectListAsync();

            if (id != Guid.Empty)
            {
                model = await _service.IGetAsync(id, new string[] { nameof(SysUserDto.SysDepartment), nameof(SysUserDto.UserRoles) });
                JsonSerializer sj = new JsonSerializer();
                var userRoleList = sj.Serialize(model.UserRoles?.Select(a => a.SysRoleId).ToList());
                ViewBag.userRoleList = userRoleList;
            }
            return View("Edit", model);
        }

        [HttpGet("details")]
        [PermissionFilter(UserPermission.UserController_details)]
        public async Task<IActionResult> Details(Guid id)
        {
            return await this.EditAsync(id);
        }

        [HttpPost("Save")]
        [PermissionFilter(UserPermission.UserController_Edit)]
        public async Task<IActionResult> SaveAsync(SysUserDto dto, string[] roles)
        {
            if (!object.Equals(roles, null))
            {
                var userRoles = new List<SysUserRoleDto>();
                foreach (var role in roles)
                {
                    userRoles.Add(new SysUserRoleDto() { SysUserId = dto.Id, SysRoleId = Guid.Parse(role) });
                }
                dto.UserRoles = userRoles;
            }
            return await base.SaveAsync(dto, new List<string>() { nameof(SysUser.Password) });
        }

        [HttpPost("DeleteMuti")]
        [PermissionFilter(UserPermission.UserController_DeleteMuti)]
        public override async Task<IActionResult> DeleteMutiAsync(string ids)
        {

            #region 验证是否是admin
            string[] idArray = ids.Split(',');
            List<Guid> delIds = new List<Guid>();
            foreach (string id in idArray)
            {
                delIds.Add(Guid.Parse(id));
            }

            var retDleteVerifyAdmin = await DleteVerifyAdmin(delIds);
            if (retDleteVerifyAdmin != null)
                return retDleteVerifyAdmin;
            #endregion
            return await base.DeleteMutiAsync(ids);
        }

        [HttpPost("Delete")]
        [PermissionFilter(UserPermission.UserController_Delete)]
        public override async Task<IActionResult> DeleteAsync(Guid id)
        {

            //验证是否是admin
            var retDleteVerifyAdmin = await DleteVerifyAdmin(new List<Guid>() { id });
            if (retDleteVerifyAdmin != null)
                return retDleteVerifyAdmin;
            return await base.DeleteAsync(id);

        }


        [HttpGet("UpdatePwd")]
        public IActionResult UpdatePwd()
        {
            return View();
        }

        [HttpPost("UpdatePwd")]
        public async Task<IActionResult> UpdatePwdAsync(UpdatePwdDto updatePwdDto)
        {
            if (ModelState.IsValid)
            {
                //取当前登录账号
                updatePwdDto.LoginName = HttpContext.User.Identity.Name;
                var ret = await _service.UpdatePwdAsync(updatePwdDto);
                //老密码验证成功后修改密码
                if (ret)
                {
                    ModelState.AddModelError("ErrMsg", "密码修改成功，点击返回主页，下次登录请使用新密码");
                }
            }
            //登录失败
            ModelState.AddModelError("ErrMsg", "用户名或密码错误");
            return View("UpdatePwd");
        }

        /// <summary>
        /// 删除时验证用户是否是admin
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        private async Task<IActionResult> DleteVerifyAdmin(List<Guid> delIds)
        {
            Guid adminId = Guid.Empty;
            var admin = (await _service.GetAllListAsync(u => u.LoginName == "admin"));
            if (admin != null && admin.Any())
            {
                adminId = (Guid)admin.FirstOrDefault()?.Id;
                if (delIds.Contains(adminId))
                {
                    return JsonFaild("admin用户不能删除");
                }
            }
            return null;
        }

        /// <summary>
        /// 生存Role SelectListItem
        /// </summary>
        /// <returns></returns>
        private async Task SetroleSelectListAsync()
        {
            var allRoleList = await _roleService.GetAllListAsync();
            var roleSelectList = allRoleList.Select(role => (new SelectListItem() { Text = role.Name, Value = role.Id.ToString() }));
            ViewBag.roleSelectList = roleSelectList;
        }

    }
}
