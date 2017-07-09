using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using LearningForAbp.EntityFramework;

namespace LearningForAbp.Migrator
{
    [DependsOn(typeof(LearningForAbpDataModule))]
    public class LearningForAbpMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<LearningForAbpDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}