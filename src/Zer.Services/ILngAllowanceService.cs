using System.Collections.Generic;
using Zer.Framework;
using Zer.GytDto;

namespace Zer.Services
{
    public interface ILngAllowanceService : IAppService
    {
        List<LngAllowanceInfoDto> GetList(FilterMatchInputDto filterMatchInput);

        List<LngAllowanceInfoDto> GetList(int[] idList);

        LngAllowanceInfoDto GetById(int id);

        List<LngAllowanceInfoDto> GetAll();

        bool Add(LngAllowanceInfoDto model);

        bool AddRange(List<LngAllowanceInfoDto> list);
    }
}
