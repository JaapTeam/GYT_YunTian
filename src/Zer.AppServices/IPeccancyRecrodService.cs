using System.Collections.Generic;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;

namespace Zer.AppServices
{
    public interface IPeccancyRecrodService : AppServices.IAppService<PeccancyRecrodDto, int>
    {
        List<PeccancyRecrodDto> GetListByFilterMatch(AppServices.FilterMatchInputDto filterMatch);

        bool ChangeStatusById(int id);

        List<PeccancyRecrodDto> GetListByIds(int[] ids);

        bool Exists(PeccancyRecrodDto overloadRecrodDto);

        List<PeccancyRecrodDto> GetList(PeccancySearchDto searchDto);

        /// <summary>
        /// 检查公司是否有未整改记录
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        bool ExistsCompanyName(string companyName);
    }
}
