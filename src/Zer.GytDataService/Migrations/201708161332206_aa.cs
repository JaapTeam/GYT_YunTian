namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GYTInfoes", "OldTruckNo", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GYTInfoes", "OldTruckNo");
        }
    }
}
