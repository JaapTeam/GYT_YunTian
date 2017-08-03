using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.AppServices
{
    public interface ICardsSuccessInfo:IAppService<CardsSuccessInfoDto,int>
    {
        List<CardsSuccessInfoDto> GetListByFilterMatch(AppServices.FilterMatchInputDto filterMatch);

        List<CardsSuccessInfoDto> GetListByIds(int[] ids);

        bool Import(List<CardsSuccessInfoDto> entityList);
    }
}
