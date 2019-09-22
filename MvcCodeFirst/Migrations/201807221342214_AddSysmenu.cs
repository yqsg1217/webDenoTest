namespace MvcCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSysmenu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SysMenu",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        ParentID = c.Int(),
                        Name = c.String(maxLength: 50),
                        Action = c.String(),
                        Controller = c.String(),
                        IconImage = c.String(),
                        MenuType = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SysMenu");
        }
    }
}
