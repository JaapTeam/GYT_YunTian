using System.Data.Entity;
using Zer.Framework.Entities;

namespace Zer.Framework.EntityFramework
{
    public abstract class EfRepositoryBase<TEntity> : EfRepositoryBase<TEntity, int>
        where TEntity : class ,IEntity<int>
    {
        protected EfRepositoryBase(DbContext dbContext) : base(dbContext)
        {
        }
    }
}