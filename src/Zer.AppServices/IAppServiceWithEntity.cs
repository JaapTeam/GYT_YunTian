using Zer.Framework.Entities;

namespace Zer.AppServices
{
    public interface  IAppService<TEntityDto>:IAppService<TEntityDto,int>
        where TEntityDto:IEntity<int>
    {
    }
}