using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Common.PagingQuery
{
    public class PagingQueryModelDesigner<TEntity>
    {
        public PagingQueryModelDesigner()
        {
        }
        public void Build(IPagingQueryModelBuild<TEntity> pagingQueryModelBuild)
        {
            pagingQueryModelBuild.MakePagingQueryModel();
        }
    }
}
