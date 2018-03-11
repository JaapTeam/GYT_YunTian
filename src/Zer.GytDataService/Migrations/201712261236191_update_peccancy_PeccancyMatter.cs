namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_peccancy_PeccancyMatter : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PeccancyInfoes", "PeccancyMatter", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PeccancyInfoes", "PeccancyMatter", c => c.String(maxLength: 20));
        }
    }
}
