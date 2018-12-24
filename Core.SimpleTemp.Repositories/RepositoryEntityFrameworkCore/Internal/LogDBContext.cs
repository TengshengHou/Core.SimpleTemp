using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Entitys.Sys;
using Microsoft.EntityFrameworkCore;

namespace Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal
{
    /// <summary>
    /// 多Db链接示例
    /// </summary>
    public class LogDBContext : DbContext
    {
    
        public LogDBContext(DbContextOptions<LogDBContext> options) : base(options)
        {

        }
 
        public DbSet<SysLoginLog> SysLoginLog { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            //启用Guid主键类型扩展
            modelBuilder.HasPostgresExtension("uuid-ossp");
        }
    }


}
