using Core.SimpleTemp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.SimpleTemp.Repository
{
    /// <summary>
    /// 基于EF Core 基础仓储
    /// </summary>
    public class CoreDBContext : DbContext
    {
        public CoreDBContext(DbContextOptions<CoreDBContext> options) : base(options)
        {

        }

        public DbSet<SysUser> SysUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }


}
