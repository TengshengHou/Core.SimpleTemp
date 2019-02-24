using Core.SimpleTemp.Application.ServiceApp.SysApp.FileApp;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Common.BaseLibrary;
using Core.SimpleTemp.Entitys.Sys;
using Core.SimpleTemp.Repositories.IRepositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.SimpleTemp.Application.UserApp
{


    public class SysFileAppService : BaseAppService<SysFileDto, SysFile, ISysFileRepository>, ISysFileAppService
    {
        WebAppOptions _webAppOptions;
        public SysFileAppService(ISysFileRepository repository, IOptionsMonitor<WebAppOptions> webAppOptions) : base(repository)
        {
            _repository = repository;
            _webAppOptions = webAppOptions.CurrentValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateFileDto"></param>
        /// <returns></returns>
        public async Task<List<SysFileDto>> UploadFileAsync(UpdateFileDto updateFileDto)
        {
            List<SysFileDto> dtoList = new List<SysFileDto>();
            if (!object.Equals(updateFileDto.files, null) && updateFileDto.files.Any())
            {
                foreach (var item in updateFileDto.files)
                {
                    var dto = new SysFileDto();
                    dto.B_ID = updateFileDto.B_ID;
                    dto.FileName = item.FileName;
                    dto.FilePath = Filehelp.GetFilePath(_webAppOptions.FileSaveBasePath);
                    dto.Extension = System.IO.Path.GetExtension(item.FileName);
                    dto.Size = item.Length;
                    dto.Type = "UUID";
                    dto.IDate = DateTime.Now;
                    dto.UDate = DateTime.Now;

                    await Filehelp.WriteFileAsync(item.OpenReadStream(), dto.FilePath);
                  
                   
                    var retDot = await InsertAsync(dto, false);
                    dtoList.Add(retDot);
                }
                await _repository.SaveAsync();
            }
            return dtoList;
        }
    }
}