using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.Services
{
    public interface IOverloadRecrodService:IDomainService<OverloadRecrodDto, int>
    {
        List<OverloadRecrodDto> GetListByFilterMatch(FilterMatchInputDto filterMatch);

        bool ChangedById(int id);

        List<OverloadRecrodDto> GetListByIds(int[] ids);
    }
}
