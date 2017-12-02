namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_manyPropertyInPeccancyInfoEntity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.PeccancyInfoes", "AxisNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PeccancyInfoes", "AxisNumber", c => c.Int(nullable: false));
        }
    }
}
