namespace MvcCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreateDateToSysDepartment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SysDepartment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        DepartmentDesc = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SysUser", "SysDepartmentId", c => c.Int());
            CreateIndex("dbo.SysUser", "SysDepartmentId");
            AddForeignKey("dbo.SysUser", "SysDepartmentId", "dbo.SysDepartment", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SysUser", "SysDepartmentId", "dbo.SysDepartment");
            DropIndex("dbo.SysUser", new[] { "SysDepartmentId" });
            DropColumn("dbo.SysUser", "SysDepartmentId");
            DropTable("dbo.SysDepartment");
        }
    }
}
