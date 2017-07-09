using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using LearningForAbp.Migrations.SeedData;
using EntityFramework.DynamicFilters;

namespace LearningForAbp.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<LearningForAbp.EntityFramework.LearningForAbpDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LearningForAbp";
        }

        protected override void Seed(LearningForAbp.EntityFramework.LearningForAbpDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();

                //default task seed (in host database).
                new DefaultTestDataForTask(context).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}
