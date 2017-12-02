namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lng_add_field_description : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LngAllowanceInfoes", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LngAllowanceInfoes", "Description");
        }
    }
}
