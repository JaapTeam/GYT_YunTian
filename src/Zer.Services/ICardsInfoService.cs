using System.Collections.Generic;
using Zer.Framework;
using Zer.GytDto;

namespace Zer.Services
{
    public interface ICardsInfoService : IAppService
    {
        List<CardsInfoDto> GetListByFilterMatch(FilterMatchInputDto filterMatch);

        List<CardsInfoDto> GetListByIds(int[] ids);

        bool AuditorByIds(int[] ids);

        bool Import(List<CardsInfoDto> entityList);

        List<CardsInfoDto> GetList(int[] idList);

        CardsInfoDto GetById(int id);

        List<CardsInfoDto> GetAll();

        bool Add(CardsInfoDto model);

        bool AddRange(List<CardsInfoDto> list);
    }
}
