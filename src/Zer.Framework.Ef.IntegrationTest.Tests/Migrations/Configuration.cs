using Zer.Framework.Entities;

namespace Zer.Framework.Ef.IntegrationTest.Tests.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Zer.Framework.Ef.IntegrationTest.Tests.TestDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Zer.Framework.Ef.IntegrationTest.Tests.TestDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.TestEntities.AddOrUpdate(
                p => p.Id,
                new TestEntity() { Id = 1, BoolenField = false, CreateTime = DateTime.Now.AddDays(-3), State = State.Active, StringField = Guid.NewGuid().ToString() },
                new TestEntity() { Id = 2, BoolenField = true, CreateTime = DateTime.Now.AddDays(-4), State = State.SoftDeleted, StringField = Guid.NewGuid().ToString() },
                new TestEntity() { Id = 3, BoolenField = true, CreateTime = DateTime.Now.AddDays(-5), State = State.Active, StringField = Guid.NewGuid().ToString() },
                new TestEntity() { Id = 4, BoolenField = false, CreateTime = DateTime.Now.AddDays(-3), State = State.Active, StringField = Guid.NewGuid().ToString() },
                new TestEntity() { Id = 5, BoolenField = true, CreateTime = DateTime.Now.AddDays(-4), State = State.Active, StringField = Guid.NewGuid().ToString() }
                );
        }
    }
}
