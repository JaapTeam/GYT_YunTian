using Zer.Framework.Entities;

namespace Zer.Framework.Repository
{
    public interface IRepository<TEntity> : IRepository<TEntity, int>
        where TEntity : class ,IEntity<int>
    { }
}