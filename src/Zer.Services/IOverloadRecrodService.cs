using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.AppServices
{
    public interface IOverloadRecrodService: AppServices.IAppService<OverloadRecrodDto, int>
    {
        List<OverloadRecrodDto> GetListByFilterMatch(AppServices.FilterMatchInputDto filterMatch);

        bool ChangedById(int id);

        List<OverloadRecrodDto> GetListByIds(int[] ids);
    }
}
