using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.AppServices
{
    public interface ICardsInfoService:IDomainService<CardsInfoDto,int>
    {
        List<CardsInfoDto> GetListByFilterMatch(AppServices.FilterMatchInputDto filterMatch);

        List<CardsInfoDto> GetListByIds(int[] ids);

        bool AuditorByIds(int[] ids);

        bool Import(List<CardsInfoDto> entityList);
    }
}
