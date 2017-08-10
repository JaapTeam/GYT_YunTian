using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.AppServices
{
    public interface IPeccancyRecrodService : AppServices.IAppService<PeccancyRecrodDto, int>
    {
        List<PeccancyRecrodDto> GetListByFilterMatch(AppServices.FilterMatchInputDto filterMatch);

        bool ChangeStatusById(int id);

        List<PeccancyRecrodDto> GetListByIds(int[] ids);

        bool Exists(PeccancyRecrodDto overloadRecrodDto);
    }
}
