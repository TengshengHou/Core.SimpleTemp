﻿using Core.SimpleTemp.Domain.Entities;
using Core.SimpleTemp.Domain.IRepositories.Internal.Data;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.SimpleTemp.Repository.Internal.Data;
using System.Linq;
using Core.SimpleTemp.Domain.IRepositories;

namespace Core.SimpleTemp.Repository
{
    public abstract partial class BaseRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="startPage">页码</param>
        /// <param name="pageSize">单页数据数</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public async Task<IPageModel<TEntity>> LoadPageListAsync(int startPage, int pageSize, Expression<Func<TEntity, bool>> where = null, Expression<Func<TEntity, object>> order = null) 
        {
            var result = from p in _dbContext.Set<TEntity>()
                         select p;
            if (where != null)
                result = result.Where(where);
            if (order != null)
                result = result.OrderBy(order);
            else
                result = result.OrderBy(m => m.Id);
            int rowCount = await result.CountAsync();
            var pageData = await result.Skip((startPage - 1) * pageSize).Take(pageSize).ToListAsync();

            var PageModel = new PageModel<TEntity>()
            {
                RowCount = rowCount,
                PageData = pageData
            };
            return PageModel;
        }
    }
}