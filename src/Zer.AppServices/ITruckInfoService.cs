using System.Collections.Generic;
using Zer.GytDto;

namespace Zer.AppServices
{
    public interface ITruckInfoService : AppServices.IAppService<TruckInfoDto, int>
    {
        
        TruckInfoDto GetByTruckNo(string truckNo);

        List<TruckInfoDto> GetListByCompanyId(int id);

        bool Exists(string truckNo);

        /// <summary>
        /// 注册集合中未注册的车辆，并返回新注册车辆的集合
        /// </summary>
        List<TruckInfoDto> QueryAfterValidateAndRegist(List<TruckInfoDto> list);

        TruckInfoDto QueryAfterValidateAndRegist(TruckInfoDto dto);
    }
}
