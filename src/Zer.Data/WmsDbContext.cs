using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Zer.Data
{
    public class WmsDbContext : DbContext
    {
      

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }


    }
}
