namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changePeccancyInfoPrimaryKey_removePeccancyId : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PeccancyInfoes", "PeccancyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PeccancyInfoes", "PeccancyId", c => c.String(maxLength: 30));
        }
    }
}
