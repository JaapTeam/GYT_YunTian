namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify_LngEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LngAllowanceInfoes", "CompanyId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LngAllowanceInfoes", "CompanyId");
        }
    }
}
