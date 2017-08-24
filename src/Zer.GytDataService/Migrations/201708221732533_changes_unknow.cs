namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes_unknow : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GYTInfoes", "OriginalCompanyId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GYTInfoes", "OriginalCompanyId", c => c.Int(nullable: false));
        }
    }
}
