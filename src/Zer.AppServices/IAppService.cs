using System.Collections.Generic;

namespace Zer.AppServices
{
    public interface IAppService<TEntityDto,in TPrimaryKey>
    {
        TEntityDto GetById(TPrimaryKey id);
        List<TEntityDto> GetAll();

        bool Add(TEntityDto model);

        bool AddRange(List<TEntityDto> list);

    }

}
