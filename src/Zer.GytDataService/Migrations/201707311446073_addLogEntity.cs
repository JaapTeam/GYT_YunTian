namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLogEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ActionType = c.Int(nullable: false),
                        ActionModel = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        MAC = c.String(),
                        IP = c.String(),
                        Content = c.String(),
                        DeleteState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logs");
        }
    }
}
