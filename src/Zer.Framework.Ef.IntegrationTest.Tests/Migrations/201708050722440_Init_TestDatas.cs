namespace Zer.Framework.Ef.IntegrationTest.Tests.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init_TestDatas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StringField = c.String(),
                        BoolenField = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        State = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TestEntities");
        }
    }
}
