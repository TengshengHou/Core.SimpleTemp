using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories.Internal.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Repository.Internal.Data
{
    /// <summary>
    /// 分页Model
    /// </summary>
    /// <typeparam name="TPageData"></typeparam>
    public class PageModel<TEntity> : IPageModel<TEntity> where TEntity : Entity
    {
        public List<TEntity> PageData { get; set; }
        public int RowCount { get; set; }
    }
}
