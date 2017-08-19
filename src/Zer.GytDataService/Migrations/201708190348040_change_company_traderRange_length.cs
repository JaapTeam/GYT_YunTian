namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_company_traderRange_length : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CompanyInfoes", "TraderRange", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CompanyInfoes", "TraderRange", c => c.String(maxLength: 20));
        }
    }
}
