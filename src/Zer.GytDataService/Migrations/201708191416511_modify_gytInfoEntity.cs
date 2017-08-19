namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify_gytInfoEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GYTInfoes", "BidDisplayName", c => c.String(maxLength: 20));
            DropColumn("dbo.GYTInfoes", "OldTruckNo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GYTInfoes", "OldTruckNo", c => c.String(maxLength: 10));
            DropColumn("dbo.GYTInfoes", "BidDisplayName");
        }
    }
}
