using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using LearningForAbp.Authorization.Roles;
using LearningForAbp.MultiTenancy;
using LearningForAbp.Tasks;
using LearningForAbp.Users;

namespace LearningForAbp.EntityFramework
{
    public class LearningForAbpDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public LearningForAbpDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in LearningForAbpDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of LearningForAbpDbContext since ABP automatically handles it.
         */
        public LearningForAbpDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public LearningForAbpDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public LearningForAbpDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }

        // TODO: define an IDbSet for your entity Tasks
        public IDbSet<Task> Tasks { get; set; }
    }
}
