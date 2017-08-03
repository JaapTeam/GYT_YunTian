using System.Collections.Generic;

namespace Zer.AppServices
{
    public abstract class AppServiceBase<TEntityDto>:IAppService<TEntityDto,int>
    {
        public abstract TEntityDto GetById(int id);

        public abstract List<TEntityDto> GetAll();
        
        public abstract bool Add(TEntityDto model);

        public abstract bool AddRange(List<TEntityDto> list);

    }
}