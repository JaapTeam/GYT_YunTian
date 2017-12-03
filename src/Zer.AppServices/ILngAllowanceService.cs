using System.Collections.Generic;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;

namespace Zer.AppServices
{
    public interface ILngAllowanceService: IAppService<LngAllowanceInfoDto,string>
    {
        bool Exists(LngAllowanceInfoDto lngAllowanceInfoDto);

        List<LngAllowanceInfoDto> GetList(LngAllowanceSearchDto searchDto);

        LngAllowanceInfoDto ChangStatus(string id);

        void AddDescription(string id, string description);

        void ForceImport(List<LngAllowanceInfoDto> list);
    }
}
