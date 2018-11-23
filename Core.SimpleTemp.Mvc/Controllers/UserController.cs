﻿using Core.SimpleTemp.Domain.Authorization;
using Core.SimpleTemp.Service.Authorization;
using Core.SimpleTemp.Service.RoleApp;
using Core.SimpleTemp.Service.UserApp;
using Core.SimpleTemp.Service.UserApp.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Mvc.Controllers
{
    [Authorize]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly ISysUserAppService _service;
        private readonly ISysRoleAppService _roleService;
        public UserController(ISysUserAppService service, ISysRoleAppService sysRoleAppService)
        {
            _service = service;
            _roleService = sysRoleAppService;
        }

        [HttpGet("Index")]
        [PermissionFilter(UserPermission.UserController_Index)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetUserByDepartment")]
        [PermissionFilter(UserPermission.UserController_GetUserByDepartment)]
        public async Task<IActionResult> GetUserByDepartmentAsync(Guid departmentId, int startPage, int pageSize)
        {
            int rowCount = 0;
            var result = await _service.GetUserByDepartmentAsync(departmentId, startPage, pageSize);
            rowCount = result.RowCount;
            result?.PageData?.ForEach(r => r.Password = null);
            var roles = await _roleService.GetAllListAsync();
            return Json(new
            {
                rowCount = rowCount,
                pageCount = Math.Ceiling(Convert.ToDecimal(rowCount) / pageSize),
                rows = result.PageData,
                roles = roles
            });
        }

        [HttpPost("Edit")]
        [PermissionFilter(UserPermission.UserController_Edit)]
        public async Task<IActionResult> EditAsync(SysUserDto dto, string roles)
        {
            try
            {
                if (!string.IsNullOrEmpty(roles))
                {
                    var userRoles = new List<SysUserRoleDto>();
                    foreach (var role in roles.Split(','))
                    {
                        userRoles.Add(new SysUserRoleDto() { SysUserId = dto.Id, SysRoleId = Guid.Parse(role) });
                    }
                    dto.UserRoles = userRoles;
                }

                var model = await _service.GetAsync(dto.Id);
                if (model == null)
                {
                    await _service.InsertAsync(dto);
                    return Json(new { Result = "Success" });
                }
                else
                {

                    await _service.UpdateAsync(dto, noUpdateProperties: new List<string> { nameof(dto.Password), nameof(dto.LoginName) });
                    return Json(new { Result = "Success" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Faild", Message = ex.Message });

            }
        }

        [HttpPost("DeleteMuti")]
        [PermissionFilter(UserPermission.UserController_DeleteMuti)]
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

                //验证是否是admin
                var retDleteVerifyAdmin = await DleteVerifyAdmin(delIds);
                if (retDleteVerifyAdmin != null)
                    return retDleteVerifyAdmin;

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
        [PermissionFilter(UserPermission.UserController_Delete)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                //验证是否是admin
                var retDleteVerifyAdmin = await DleteVerifyAdmin(new List<Guid>() { id });
                if (retDleteVerifyAdmin != null)
                    return retDleteVerifyAdmin;

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
        [PermissionFilter(UserPermission.UserController_Get)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var dto = await _service.GetAsync(id);
            return Json(dto);
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
                    return Json(new
                    {
                        Result = "Faild",
                        Message = "admin用户不能删除"
                    });
                }
            }
            return null;
        }
    }
}
