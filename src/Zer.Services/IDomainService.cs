using Zer.Entities;

namespace Zer.Services
{
    public interface  IDomainService<TEntity>:IDomainService<TEntity,int>
        where TEntity:IEntity<int>
    {
    }
}