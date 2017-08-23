using System.Collections.Generic;

namespace Zer.AppServices
{
    public abstract class AppServiceBase<TEntityDto>:IAppService<TEntityDto,int>
    {
        public abstract TEntityDto GetById(int id);

        public abstract List<TEntityDto> GetAll();
        
        public abstract TEntityDto Add(TEntityDto model);

        public abstract List<TEntityDto> AddRange(List<TEntityDto> list);
        public abstract TEntityDto Edit(TEntityDto model);
    }
}