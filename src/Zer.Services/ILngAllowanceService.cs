using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.AppServices
{
    public interface ILngAllowanceService: AppServices.IDomainService<LngAllowanceInfoDto,int>
    {
        List<LngAllowanceInfoDto> GetList(AppServices.FilterMatchInputDto filterMatchInput);

        List<LngAllowanceInfoDto> GetList(int[] idList);
    }
}
