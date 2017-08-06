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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LngAllowanceInfoes");
        }
    }
}
