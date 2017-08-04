using Zer.Framework.Entities;
using Zer.Framework.EntityFramework;

namespace Zer.GytDataService
{
    public class GytRepository<TEntity> : EfRepositoryBase<TEntity>
        where TEntity : class, IEntity<int>
    {
        public GytRepository() : base(new GytDbContext())
        {
        }
    }
}