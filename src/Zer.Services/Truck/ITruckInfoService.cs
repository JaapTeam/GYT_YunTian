using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zer.Services.Truck.Dto;

namespace Zer.Services.Truck
{
    public interface ITruckInfoService : IDomainService<TruckInfoDto, int>
    {
        
        TruckInfoDto GetByTruckNo(string truckNo);

        List<TruckInfoDto> GetListByCompanyId(int id);

        bool TruckNoExists(string truckNo);
    }
}
