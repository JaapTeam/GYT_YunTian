using System.Data.Entity;
using Zer.Entities;
using Zer.Framework.EntityFramework;

namespace Zer.GytDataService.Impl
{
    public class LogDataService : EFRepositoryBase<Log, int>, ILogDataService
    {
        public LogDataService() : base(new GytDbContext())
        {
        }
    }
}