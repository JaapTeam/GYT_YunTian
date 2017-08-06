namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify_TruckInfo_DriverId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TruckInfoes", "DriverId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TruckInfoes", "DriverId", c => c.Int(nullable: false));
        }
    }
}
