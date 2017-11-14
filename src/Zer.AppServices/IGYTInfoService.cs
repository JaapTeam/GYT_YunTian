using System.Collections.Generic;
using Zer.Entities;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;

namespace Zer.AppServices
{
    public interface IGYTInfoService : IAppService<GYTInfoDto, string>
    {
        bool Exists(string bidTruckNo);
        
        List<GYTInfoDto> GetListByBidTruckNoList(List<string> bidTruckNoList);
        List<GYTInfoDto> GetList(GYTInfoSearchDto searchDto);

        GYTInfoDto GetByBidTruckNo(string bidTruckNo);

        /// <summary>
        /// 检查车牌以旧换新指标是否已使用
        /// </summary>
        /// <param name="truckNo"></param>
        /// <returns></returns>
        bool TargetIsUse(string truckNo);

        GYTInfoDto Verify(string infoId);

        void SetStatus(string truckNo, BusinessState state);
    }
}