using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Entitys.Script;
using Core.SimpleTemp.Entitys.Sys;
using Microsoft.EntityFrameworkCore;

namespace Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal
{
    /// <summary>
    /// 多Db链接示例
    /// </summary>
    public class BusinessDBContext : DbContext
    {

        public DbSet<Script> Script { get; set; }
        public DbSet<ScriptDetails> ScriptDetails { get; set; }

        public BusinessDBContext(DbContextOptions<BusinessDBContext> options) : base(options)
        {
         
        }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            //启用Guid主键类型扩展
            modelBuilder.HasPostgresExtension("uuid-ossp");
        }
    }


}
