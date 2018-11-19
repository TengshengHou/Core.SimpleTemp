using System;
using System.Collections.Generic;
using AutoMapper;
using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories;
using Core.SimpleTemp.Service.UserApp.Dto;
using System.Threading.Tasks;
using Core.SimpleTemp.Domain.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.Internal.Data;

namespace Core.SimpleTemp.Service.UserApp
{
    public class SysUserAppService : BaseAppService<SysUserDto, SysUser, ISysUserRepository>, ISysUserAppService
    {
        public SysUserAppService(ISysUserRepository repository) : base(repository)
        {
        }

        public async Task<IPageModel<SysUserDto>> GetUserByDepartmentAsync(Guid departmentId, int startPage, int pageSize)
        {
            var pageModel = await _repository.LoadPageListAsync(startPage, pageSize, it => it.SysDepartmentId == departmentId);
            IPageModel<SysUserDto> pageModelDto = new PageModel<SysUserDto>()
            {
                PageData = Mapper.Map<List<SysUserDto>>(pageModel.PageData),
                RowCount = pageModel.RowCount
            };
            return pageModelDto;
        }
      
    }
}
