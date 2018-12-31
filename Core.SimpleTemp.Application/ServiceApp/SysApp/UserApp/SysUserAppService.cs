using AutoMapper;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal.Data;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application.UserApp
{


    public class SysUserAppService : BaseAppService<SysUserDto, SysUser, ISysUserRepository>, ISysUserAppService
    {
        readonly WebAppOptions _webAppOptions;
        public SysUserAppService(ISysUserRepository repository, IOptionsMonitor<WebAppOptions> webAppOptions) : base(repository)
        {
            _webAppOptions = webAppOptions.CurrentValue;
        }

        public async Task<IPageModel<SysUserDto>> GetUserByDepartmentAsync(Guid departmentId, int startPage, int pageSize)
        {
            var pageModel = await GetAllPageListAsync(startPage, pageSize, it => it.SysDepartmentId == departmentId);
            IPageModel<SysUserDto> pageModelDto = new PageModel<SysUserDto>()
            {
                PageData = Mapper.Map<List<SysUserDto>>(pageModel.PageData),
                RowCount = pageModel.RowCount
            };
            return pageModelDto;
        }


        public async Task<bool> UpdatePwdAsync(UpdatePwdDto dto)
        {
            var userEntity = await FirstOrDefaultEntityAsync(user => user.LoginName == dto.LoginName && user.Password == dto.OldPwd);

            if (userEntity == null)
                return false;
            userEntity.Password = dto.NewPwdTow;
            await UpdateAsync(userEntity, true);
            return true;
        }


        public async Task RestoreUserPwdAsync(string LoginName)
        {
            var entity = await FirstOrDefaultEntityAsync(u => u.LoginName == LoginName);
            entity.Password = _webAppOptions.InitialPassword;
            await UpdateAsync(entity, true);
        }


        public Task<SysUserRole> FindFirstUserRoleByRoleIdsAsync(Guid[] roleIds)
        {
            return _repository.FindFirstUserRoleByRoleIdsAsync(roleIds);
        }
    }
}
