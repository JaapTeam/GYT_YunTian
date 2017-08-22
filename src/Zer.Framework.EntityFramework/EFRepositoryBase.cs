using System.Data.Entity;
using Zer.Framework.Entities;

namespace Zer.Framework.EntityFramework
{
    public abstract class EfRepositoryBase<TEntity> : EfRepositoryBaseWithPrimaryKey<TEntity, int>
        where TEntity : class ,IEntity<int>
    {
        protected EfRepositoryBase(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public abstract class EfRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBaseWithPrimaryKey<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        public EfRepositoryBase(DbContext dbContext)
            : base(dbContext)
        {
        }

        public abstract TPrimaryKey GeneratePrimaryKey();

        public override TEntity Insert(TEntity entity)
        {
            entity.Id = GeneratePrimaryKey();
            return base.Insert(entity);
        }
    }
}