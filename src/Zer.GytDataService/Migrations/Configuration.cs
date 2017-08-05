using Zer.Entities;

namespace Zer.GytDataService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Zer.GytDataService.GytDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Zer.GytDataService.GytDbContext context)
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

            context.CompanyInfos.AddOrUpdate(
                p=>p.Id,
                new CompanyInfo() { CompanyName = "深圳市万事达物流有限公司", TraderRange = "普通货运，货物专用运输（集装箱）" },
                new CompanyInfo() { CompanyName = "深圳市金点通贸易有限公司", TraderRange = "货物专用运输（集装箱）" },
                new CompanyInfo() { CompanyName = "深圳市致远物流有限公司", TraderRange = "普通货运，货物专用运输（罐式）" }
                );
        }
    }
}
