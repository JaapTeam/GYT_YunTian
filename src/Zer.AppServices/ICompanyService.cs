using System.Collections.Generic;
using Zer.Entities;
using Zer.GytDto;
using Zer.GytDto.SearchFilters;

namespace Zer.AppServices
{
    public interface ICompanyService : IAppService<CompanyInfoDto,int>
    {
        #region query

        /// <summary>
        /// 模糊查询，输入公司名称关键字，查询包含该字符的公司信息
        /// </summary>
        /// <param name="likeName">公司名称关键字</param>
        /// <returns>an array of <see cref="Zer.GytDto.CompanyInfoDto"/></returns>
        List<CompanyInfoDto> GetByLikeName(string likeName);

        /// <summary>
        /// 判断公司是否存在
        /// </summary>
        /// <param name="companyFullName">完整公司名称</param>
        /// <returns></returns>
        bool Exists(string companyFullName);

        #endregion

        void Add(CompanyInfo model);

        /// <summary>
        /// 检查公司是否存在，如果不存在新增，在完成所有新增操作后，查询并返回参数指定公司信息
        /// </summary>
        /// <param name="companyInfoDtos"></param>
        /// <returns><see cref="List{CompanyInfoDto}"/></returns>
        List<CompanyInfoDto> QueryAfterValidateAndRegist(List<CompanyInfoDto> companyInfoDtos);

        List<CompanyInfoDto> GetWithPeccancyRecored(CompanySearchDto filter);
    }
}
