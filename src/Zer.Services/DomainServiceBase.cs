using System.Collections.Generic;
using Zer.Entities;

namespace Zer.Services
{
    public abstract class DomainServiceBase<TEntityDto>:IDomainService<TEntityDto,int>
    {

        public abstract TEntityDto GetById(int id);

        public abstract List<TEntityDto> GetAll();
        
        public abstract bool Add(TEntityDto model);

        public abstract bool AddRange(List<TEntityDto> list);

    }
}