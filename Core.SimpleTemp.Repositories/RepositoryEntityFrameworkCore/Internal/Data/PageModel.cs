using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Repositories.IRepositories.Internal.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal.Data
{
    /// <summary>
    /// 分页Model
    /// </summary>
    /// <typeparam name="TPageData"></typeparam>
    public class PageModel<TEntity> : IPageModel<TEntity> 
    {
        public List<TEntity> PageData { get; set; }
        public int RowCount { get; set; }
    }
}
