using Core.SimpleTemp.Entitys;
using Core.SimpleTemp.Entitys.Sys;
using Microsoft.EntityFrameworkCore;

namespace Core.SimpleTemp.Repository.RepositoryEntityFrameworkCore.Internal
{
    /// <summary>
    /// 基于EF Core 基础仓储
    /// </summary>
    public class CoreDBContext : DbContext
    {
        //static CoreDBContext()
        //{
        //    Database.SetInitializer(new DropCreateDatabaseIfModelChanges<PortalContext>());
        //}
        public CoreDBContext(DbContextOptions<CoreDBContext> options) : base(options)
        {

        }
        public DbSet<SysDepartment> SysDepartment { get; set; }
        public DbSet<SysMenu> SysMenu { get; set; }
        public DbSet<SysRole> SysRole { get; set; }
        public DbSet<SysRoleMenu> SysRoleMenu { get; set; }
        public DbSet<SysUser> SysUser { get; set; }
        public DbSet<SysUserRole> SysUserRole { get; set; }
        public DbSet<SysLoginLog> SysLoginLog { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //UserRole组合主键
            modelBuilder.Entity<SysUserRole>()
              .HasKey(ur => new { ur.SysUserId, ur.SysRoleId });

            //RoleMenu组合主键
            modelBuilder.Entity<SysRoleMenu>()
              .HasKey(rm => new { rm.SysRoleId, rm.SysMenuId });

            //启用Guid主键类型扩展
            modelBuilder.HasPostgresExtension("uuid-ossp");
        }
    }


}
