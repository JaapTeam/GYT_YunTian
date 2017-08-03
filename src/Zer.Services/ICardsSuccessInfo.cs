using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.Services
{
    public interface ICardsSuccessInfo:IDomainService<CardsSuccessInfoDto,int>
    {
        List<CardsSuccessInfoDto> GetListByFilterMatch(FilterMatchInputDto filterMatch);

        List<CardsSuccessInfoDto> GetListByIds(int[] ids);

        bool Import(List<CardsSuccessInfoDto> entityList);
    }
}
