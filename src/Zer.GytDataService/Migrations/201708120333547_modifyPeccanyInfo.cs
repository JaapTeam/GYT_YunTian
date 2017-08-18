namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyPeccanyInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PeccancyInfoes", "CompanyName", c => c.String(maxLength: 100));
            AddColumn("dbo.PeccancyInfoes", "TruckId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PeccancyInfoes", "TruckId");
            DropColumn("dbo.PeccancyInfoes", "CompanyName");
        }
    }
}
