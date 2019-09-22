using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MvcCodeFirst.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MvcCodeFirst.DAL
{
    public class AccountContext : DbContext
    {
        public AccountContext() : base("name=AccountContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }
        public DbSet<SysUserRole> SysUserRoles { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<SysDepartment> SysDepartments { get; set; }
        public DbSet<SysMenu> SysMenus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //指定表名为单数形式
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //一对多,一个部门有多个用户
            // modelBuilder.Entity<SysDepartment>().HasMany(x => x.SysUsers).WithRequired(x => x.SysDepartment);
            //多对一，多个用户属于一个部门
           // modelBuilder.Entity<SysUser>().HasRequired(x => x.SysDepartment).WithMany(x => x.SysUsers);
        }
    }
}