using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.SimpleTemp.Repository
{
    /// <summary>
    /// 基于EF Core 基础仓储
    /// </summary>
    public class BaseRepository : DbContext
    {
        public BaseRepository(DbContextOptions<BaseRepository> options) : base(options) {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }
    }

    
}
