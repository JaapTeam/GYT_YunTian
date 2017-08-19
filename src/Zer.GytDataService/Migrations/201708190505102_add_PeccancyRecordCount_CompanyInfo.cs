namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_PeccancyRecordCount_CompanyInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CompanyInfoes", "PeccancyRecordCount", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CompanyInfoes", "PeccancyRecordCount");
        }
    }
}
