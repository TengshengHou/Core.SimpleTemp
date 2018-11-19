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
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetUserByDepartment")]

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

                    await _service.UpdateAsync(dto, noUpdateProperties: new List<string> { nameof(dto.Password) });
                    return Json(new { Result = "Success" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Faild", Message = ex.Message });

            }
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
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var dto = await _service.GetAsync(id);
            return Json(dto);
        }
    }
}
