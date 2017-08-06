using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.AppServices
{
    public interface ITruckInfoService : AppServices.IAppService<TruckInfoDto, int>
    {
        
        TruckInfoDto GetByTruckNo(string truckNo);

        List<TruckInfoDto> GetListByCompanyId(int id);

        bool Exists(string truckNo);
    }
}
