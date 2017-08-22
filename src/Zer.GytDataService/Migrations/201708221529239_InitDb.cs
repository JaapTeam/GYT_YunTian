namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(maxLength: 50),
                        TraderRange = c.String(maxLength: 200),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GYTInfoes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 30),
                        BusinessType = c.Int(nullable: false),
                        OriginalCompanyId = c.Int(nullable: false),
                        OriginalCompanyName = c.String(maxLength: 50),
                        OriginalTruckNo = c.String(maxLength: 10),
                        BidCompanyId = c.Int(nullable: false),
                        BidCompanyName = c.String(maxLength: 50),
                        BidTruckNo = c.String(maxLength: 10),
                        BidDate = c.DateTime(),
                        BidName = c.String(maxLength: 20),
                        BidDisplayName = c.String(maxLength: 20),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LngAllowanceInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        CompanyName = c.String(maxLength: 50),
                        LotId = c.Int(nullable: false),
                        TruckNo = c.String(maxLength: 10),
                        EngineId = c.String(maxLength: 15),
                        CylinderDefaultId = c.String(maxLength: 20),
                        CylinderSeconedId = c.String(maxLength: 20),
                        Status = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserLogInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionType = c.Int(nullable: false),
                        ActionModel = c.String(maxLength: 50),
                        Content = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        IP = c.String(maxLength: 20),
                        UserId = c.Int(nullable: false),
                        DisplayName = c.String(maxLength: 20),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PeccancyInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PeccancyId = c.String(maxLength: 30),
                        CompanyId = c.Int(nullable: false),
                        CompanyName = c.String(maxLength: 50),
                        TruckId = c.Int(nullable: false),
                        FrontTruckNo = c.String(maxLength: 20),
                        BehindTruckNo = c.String(maxLength: 20),
                        DriverId = c.String(maxLength: 20),
                        DriverName = c.String(maxLength: 20),
                        PeccancyDate = c.DateTime(),
                        PeccancyMatter = c.String(maxLength: 20),
                        GrossWeight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AxisNumber = c.Int(nullable: false),
                        Source = c.String(maxLength: 50),
                        Status = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SystemLogInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Content = c.String(),
                        ActionName = c.String(maxLength: 20),
                        ControllerName = c.String(maxLength: 20),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TruckInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FrontTruckNo = c.String(maxLength: 10),
                        BehindTruckNo = c.String(maxLength: 10),
                        CompanyId = c.Int(nullable: false),
                        DriverId = c.String(maxLength: 20),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 20),
                        DisplayName = c.String(maxLength: 20),
                        Password = c.String(maxLength: 50),
                        UserState = c.Int(nullable: false),
                        Email = c.String(maxLength: 40),
                        MobilePhone = c.String(maxLength: 20),
                        Role = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserInfoes");
            DropTable("dbo.TruckInfoes");
            DropTable("dbo.SystemLogInfoes");
            DropTable("dbo.PeccancyInfoes");
            DropTable("dbo.UserLogInfoes");
            DropTable("dbo.LngAllowanceInfoes");
            DropTable("dbo.GYTInfoes");
            DropTable("dbo.CompanyInfoes");
        }
    }
}
