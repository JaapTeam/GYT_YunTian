using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.Services
{
    public interface ITruckInfoService : IDomainService<TruckInfoDto, int>
    {
        
        TruckInfoDto GetByTruckNo(string truckNo);

        List<TruckInfoDto> GetListByCompanyId(int id);

        bool TruckNoExists(string truckNo);
    }
}
