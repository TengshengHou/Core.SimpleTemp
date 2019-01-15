using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Common.PagingQuery
{
    public interface IPagingQueryModelBuild<TEntity>
    {
        void MakePagingQueryModel();


        PagingQueryModel<TEntity> GetPaginQueryModel();
    }
}
