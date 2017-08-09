using Zer.Framework.Entities;
using Zer.Framework.EntityFramework;

namespace Zer.GytDataService
{
    public  class  LogsRepository<TEntity>:EfRepositoryBase<TEntity>
        where TEntity : class, IEntity<int>
    {
        protected LogsRepository()
            : base(new LogsDbContext())
        {
        }
    }
}