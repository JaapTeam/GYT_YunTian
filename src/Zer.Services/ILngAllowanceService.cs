using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.Services
{
    public interface ILngAllowanceService:IDomainService<LngAllowanceInfoDto,int>
    {
        List<LngAllowanceInfoDto> GetList(FilterMatchInputDto filterMatchInput);

        List<LngAllowanceInfoDto> GetList(int[] idList);
    }
}
