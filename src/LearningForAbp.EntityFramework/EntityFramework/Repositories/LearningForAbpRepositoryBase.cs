using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace LearningForAbp.EntityFramework.Repositories
{
    public abstract class LearningForAbpRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<LearningForAbpDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected LearningForAbpRepositoryBase(IDbContextProvider<LearningForAbpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class LearningForAbpRepositoryBase<TEntity> : LearningForAbpRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected LearningForAbpRepositoryBase(IDbContextProvider<LearningForAbpDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
