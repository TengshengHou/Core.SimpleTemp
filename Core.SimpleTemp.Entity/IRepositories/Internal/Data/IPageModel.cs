using Core.SimpleTemp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Domain.IRepositories.Internal.Data
{
    public interface IPageModel<TEntity>
    {
        List<TEntity> PageData { get; set; }
        int RowCount { get; set; }
    }
}
