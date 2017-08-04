using System.Data.Entity;
using Zer.Entities;

namespace Zer.GytDataService
{
    public class GytDbContext : DbContext
    {
        public GytDbContext()
            :base("GytDbContext")
        {
            
        }
        public GytDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            
        }

        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<TruckInfo> TruckInfos { get; set; }
        public DbSet<OverloadInfo> OverloadInfos { get; set; }
        public DbSet<CompanyInfo> CompanyInfos { get; set; }
        public DbSet<LogInfo> Logs { get; set; }
    }
}
