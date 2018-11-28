using Core.SimpleTemp.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Repositories.IRepositories.Internal.Data
{
    public interface IPageModel<TEntity>
    {
        List<TEntity> PageData { get; set; }
        int RowCount { get; set; }
    }
}
