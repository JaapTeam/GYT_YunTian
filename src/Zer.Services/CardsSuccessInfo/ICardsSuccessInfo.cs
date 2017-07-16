using System.Collections.Generic;
using Zer.Services.CardsSuccessInfo.Dto;

namespace Zer.Services.CardsSuccessInfo
{
    public interface ICardsSuccessInfo:IDomainService<CardsSuccessInfoDto,int>
    {
        List<CardsSuccessInfoDto> GetListByFilterMatch(FilterMatchInputDto filterMatch);

        List<CardsSuccessInfoDto> GetListByIds(int[] ids);

        bool Import(List<CardsSuccessInfoDto> entityList);
    }
}
