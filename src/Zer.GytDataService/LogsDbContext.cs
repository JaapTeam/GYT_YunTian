using System.Data.Entity;
using Zer.Entities;

namespace Zer.GytDataService
{
    public class LogsDbContext : DbContext
    {
        public LogsDbContext()
            : base("LogsDbContext")
        {
        }

        public DbSet<UserLogInfo> Logs { get; set; }
    }
}