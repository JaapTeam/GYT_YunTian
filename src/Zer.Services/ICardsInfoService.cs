using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.Services
{
    public interface ICardsInfoService:IDomainService<CardsInfoDto,int>
    {
        List<CardsInfoDto> GetListByFilterMatch(FilterMatchInputDto filterMatch);

        List<CardsInfoDto> GetListByIds(int[] ids);

        bool AuditorByIds(int[] ids);

        bool Import(List<CardsInfoDto> entityList);
    }
}
