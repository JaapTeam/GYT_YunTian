using System.Collections.Generic;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;

namespace Zer.AppServices
{
    public interface IPeccancyRecrodService : AppServices.IAppService<PeccancyRecrodDto, int>
    {
        List<PeccancyRecrodDto> GetListByFilterMatch(AppServices.FilterMatchInputDto filterMatch);

        bool ChangeStatusById(int id);

        List<PeccancyRecrodDto> GetListByIds(int[] ids);

        bool Exists(PeccancyRecrodDto overloadRecrodDto);

        List<PeccancyRecrodDto> GetList(PeccancySearchDto searchDto);
    }
}
