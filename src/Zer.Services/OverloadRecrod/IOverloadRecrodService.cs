using System.Collections.Generic;
using Zer.Services.OverloadRecrod.Dto;

namespace Zer.Services.OverloadRecrod
{
    public interface IOverloadRecrodService:IDomainService<OverloadRecrodDto, int>
    {
        List<OverloadRecrodDto> GetListByFilterMatch(FilterMatchInputDto filterMatch);

        bool ChangedById(int id);

        List<OverloadRecrodDto> GetListByIds(int[] ids);
    }
}
