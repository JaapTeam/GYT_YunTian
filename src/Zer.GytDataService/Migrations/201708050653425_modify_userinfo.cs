namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify_userinfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LngAllowanceInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        LotId = c.Int(nullable: false),
                        TruckNo = c.String(),
                        EngineId = c.String(),
                        CylinderDefaultId = c.String(),
                        CylinderSeconedId = c.String(),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.UserInfoes", "Email", c => c.String(maxLength: 40));
            AddColumn("dbo.UserInfoes", "MobilePhone", c => c.String(maxLength: 40));
            AlterColumn("dbo.UserInfoes", "UserName", c => c.String(maxLength: 20));
            AlterColumn("dbo.UserInfoes", "DisplayName", c => c.String(maxLength: 20));
            AlterColumn("dbo.UserInfoes", "Password", c => c.String(maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserInfoes", "Password", c => c.String());
            AlterColumn("dbo.UserInfoes", "DisplayName", c => c.String());
            AlterColumn("dbo.UserInfoes", "UserName", c => c.String());
            DropColumn("dbo.UserInfoes", "MobilePhone");
            DropColumn("dbo.UserInfoes", "Email");
            DropTable("dbo.LngAllowanceInfoes");
        }
    }
}
