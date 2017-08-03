using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.AppServices
{
    public interface ITruckInfoService : AppServices.IDomainService<TruckInfoDto, int>
    {
        
        TruckInfoDto GetByTruckNo(string truckNo);

        List<TruckInfoDto> GetListByCompanyId(int id);

        bool TruckNoExists(string truckNo);
    }
}
