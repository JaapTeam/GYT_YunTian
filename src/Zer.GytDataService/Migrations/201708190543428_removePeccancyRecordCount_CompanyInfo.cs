namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removePeccancyRecordCount_CompanyInfo : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CompanyInfoes", "PeccancyRecordCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CompanyInfoes", "PeccancyRecordCount", c => c.Int());
        }
    }
}
