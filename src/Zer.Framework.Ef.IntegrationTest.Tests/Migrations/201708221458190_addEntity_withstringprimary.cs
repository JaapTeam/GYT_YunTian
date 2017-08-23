namespace Zer.Framework.Ef.IntegrationTest.Tests.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEntity_withstringprimary : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestEntityWithStringPrimaryKeys",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TestEntityWithStringPrimaryKeys");
        }
    }
}
