using Zer.Framework.Entities;

namespace Zer.AppServices
{
    public interface  IDomainService<TEntity>:IDomainService<TEntity,int>
        where TEntity:IEntity<int>
    {
    }
}