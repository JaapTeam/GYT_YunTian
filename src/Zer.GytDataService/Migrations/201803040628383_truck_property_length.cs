namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class truck_property_length : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TruckInfoes", "FrontTruckNo", c => c.String(maxLength: 20));
            AlterColumn("dbo.TruckInfoes", "BehindTruckNo", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TruckInfoes", "BehindTruckNo", c => c.String(maxLength: 10));
            AlterColumn("dbo.TruckInfoes", "FrontTruckNo", c => c.String(maxLength: 10));
        }
    }
}
