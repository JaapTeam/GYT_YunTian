using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zer.Services.CardsInfo.Dto;

namespace Zer.Services.CardsIfo
{
    public interface ICardsInfoService:IDomainService<CardsInfoDto,int>
    {
        List<CardsInfoDto> GetListByFilterMatch(FilterMatchInputDto filterMatch);

        List<CardsInfoDto> GetListByIds(int[] ids);

        bool AuditorByIds(int[] ids);

        bool Import(List<CardsInfoDto> entityList);
    }
}
