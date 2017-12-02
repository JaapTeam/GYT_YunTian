namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCreateTimeForLngInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LngAllowanceInfoes", "CreateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LngAllowanceInfoes", "CreateTime");
        }
    }
}
