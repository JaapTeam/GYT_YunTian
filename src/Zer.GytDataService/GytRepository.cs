using System.Collections.Generic;
using System.Data.Entity;
using Zer.Framework.Entities;
using Zer.Framework.EntityFramework;

namespace Zer.GytDataService
{
    public class GytRepository<TEntity> : EfRepositoryBase<TEntity>
        where TEntity : class, IEntity<int>
    {
        protected GytRepository() : base(new GytDbContext())
        {
        }
    }

    public abstract class GytRepository<TEntity,TPrimaryKey> : EfRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected GytRepository()
            : base(new GytDbContext())
        {
        }
    }
}