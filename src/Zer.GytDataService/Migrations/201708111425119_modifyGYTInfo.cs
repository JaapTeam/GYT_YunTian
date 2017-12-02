namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyGYTInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GYTInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BusinessType = c.Int(nullable: false),
                        OriginalCompanyId = c.Int(nullable: false),
                        OriginalCompanyName = c.String(maxLength: 100),
                        OriginalTruckNo = c.String(maxLength: 20),
                        BidCompanyId = c.Int(nullable: false),
                        BidCompanyName = c.String(maxLength: 100),
                        BidTruckNo = c.String(maxLength: 20),
                        BidDate = c.DateTime(),
                        BidName = c.String(maxLength: 20),
                        Status = c.Int(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GYTInfoes");
        }
    }
}
