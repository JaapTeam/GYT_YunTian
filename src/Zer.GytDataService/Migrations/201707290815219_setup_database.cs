namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setup_database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        TraderRange = c.String(),
                        DeleteState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OverloadInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        FrontTruckNo = c.String(),
                        BehindTruckNo = c.String(),
                        DriverId = c.Int(nullable: false),
                        OverloadDateTime = c.DateTime(nullable: false),
                        OverloadMatter = c.Int(nullable: false),
                        GrossWeight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AxisNumber = c.Int(nullable: false),
                        Source = c.String(),
                        DeleteState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TruckInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FrontTruckNo = c.String(),
                        RearTruckNo = c.String(),
                        CompanyId = c.Int(nullable: false),
                        DriverId = c.Int(nullable: false),
                        DeleteState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserState = c.Int(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        UserName = c.String(),
                        DisplayName = c.String(),
                        Password = c.String(),
                        DeleteState = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInfoes");
            DropTable("dbo.TruckInfoes");
            DropTable("dbo.OverloadInfoes");
            DropTable("dbo.CompanyInfoes");
        }
    }
}
