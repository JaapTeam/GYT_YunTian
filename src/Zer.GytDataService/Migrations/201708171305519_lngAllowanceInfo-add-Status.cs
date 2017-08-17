namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lngAllowanceInfoaddStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LngAllowanceInfoes", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LngAllowanceInfoes", "Status");
        }
    }
}
