namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lng_add_field_IsForceImport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LngAllowanceInfoes", "IsForceImport", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LngAllowanceInfoes", "IsForceImport");
        }
    }
}
