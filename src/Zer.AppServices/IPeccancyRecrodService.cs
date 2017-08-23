using System.Collections.Generic;
using Zer.GytDto;
using Zer.GytDto.OutputDto;
using Zer.GytDto.SearchFilters;

namespace Zer.AppServices
{
    public interface IPeccancyRecrodService : IAppService<PeccancyRecrodDto, int>
    {
        bool ChangeStatusById(string id);

        List<PeccancyRecrodDto> GetListByIds(int[] ids);

        bool Exists(PeccancyRecrodDto overloadRecrodDto);

        List<PeccancyRecrodDto> GetList(PeccancySearchDto searchDto);

        List<PeccancyGroupByCompanyDto> GetPeccancyGroupByCompany(PeccancyWithCompanySearchDto filter);

        /// <summary>
        /// 检查公司是否有未整改记录
        /// </summary>
        /// <param name="companyName"></param>
        /// <returns></returns>
        bool ExistsCompanyName(string companyName);

        PeccancyRecrodDto GetByPeccancyId(string peccancyId);
    }
}
