using AutoMapper;
using Core.SimpleTemp.Application.ServiceApp.SysApp.FileApp;
using Core.SimpleTemp.Common;
using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Entitys.Sys;
using Core.SimpleTemp.Repositories.IRepositories;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal.Data;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SimpleLibrary;
using SimpleLibrary.File;

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



        private async Task<int> SaveFileAsync(System.IO.Stream stream, string path)
        {
            int writeCount = 0;
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                byte[] byteArr = new byte[500];
                int readCount = 0;
                while ((readCount = await stream.ReadAsync(byteArr, 0, byteArr.Length)) > 0)
                {
                    await fileStream.WriteAsync(byteArr, 0, readCount);
                    writeCount += readCount;
                }
            }
            return writeCount;
        }
    }
}