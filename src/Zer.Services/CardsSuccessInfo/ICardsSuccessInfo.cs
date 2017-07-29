using System.Collections.Generic;
using Zer.Framework;
using Zer.Services.CardsSuccessInfo.Dto;

namespace Zer.Services.CardsSuccessInfo
{
    public interface ICardsSuccessInfo : IAppService
    {
        List<CardsSuccessInfoDto> GetListByFilterMatch(FilterMatchInputDto filterMatch);

        List<CardsSuccessInfoDto> GetListByIds(int[] ids);

        bool Import(List<CardsSuccessInfoDto> entityList);

        List<CardsSuccessInfoDto> GetList(int[] idList);

        CardsSuccessInfoDto GetById(int id);

        List<CardsSuccessInfoDto> GetAll();

        bool Add(CardsSuccessInfoDto model);

        bool AddRange(List<CardsSuccessInfoDto> list);
    }
}
