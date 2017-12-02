using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Zer.Framework.Entities;
using Zer.Framework.Extensions;

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


        public override IEnumerable<TEntity> AddRange(IEnumerable<TEntity> list)
        {
            foreach (var entity in list.Where(x=>x.Id == null || x.Id.ToString().IsNullOrEmpty()))
            {
                entity.Id = GeneratePrimaryKey();
            }
            return base.AddRange(list);
        }
    }
}