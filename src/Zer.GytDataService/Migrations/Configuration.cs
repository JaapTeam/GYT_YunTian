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
                new CompanyInfo() { CompanyName = "���������´��������޹�˾", TraderRange = "��ͨ���ˣ�����ר�����䣨��װ�䣩" },
                new CompanyInfo() { CompanyName = "�����н��ͨó�����޹�˾", TraderRange = "����ר�����䣨��װ�䣩" },
                new CompanyInfo() { CompanyName = "��������Զ�������޹�˾", TraderRange = "��ͨ���ˣ�����ר�����䣨��ʽ��" }
                );
        }
    }
}
