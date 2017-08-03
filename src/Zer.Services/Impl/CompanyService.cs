using System.Collections.Generic;
using AutoMapper;
using Zer.Entities;
using Zer.GytDataService;
using Zer.GytDto;

namespace Zer.AppServices.Impl
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyInfoDataService _companyInfoDataService;

        public CompanyService(ICompanyInfoDataService companyInfoDataService)
        {
            _companyInfoDataService = companyInfoDataService;
        }

        public CompanyInfoDto GetById(int id)
        {
//            var result = _companyInfoDataService.GetById(id);
//            return new CompanyInfoDto()
//            {
//                CompanyName =  result.CompanyName,
//                Id = result.Id,
//                TraderRange = result.TraderRange
//            };
            return Mapper.Map<CompanyInfoDto>(_companyInfoDataService.GetById(id));
        }

        public List<CompanyInfoDto> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public bool Add(CompanyInfoDto model)
        {
            throw new System.NotImplementedException();
        }

        public bool AddRange(List<CompanyInfoDto> list)
        {
            throw new System.NotImplementedException();
        }

        public List<CompanyInfoDto> GetByLikeName(string likeName)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(string companyFullName)
        {
            throw new System.NotImplementedException();
        }

        public void Add(CompanyInfo model)
        {
            throw new System.NotImplementedException();
        }

        public bool AddRange(List<CompanyInfo> list)
        {
            throw new System.NotImplementedException();
        }
    }
}