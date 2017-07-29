using System;
using System.Collections.Generic;
using Zer.Entities;
using Zer.GytDataService;
using Zer.GytDataService.Impl;
using Zer.Services.Company.Dto;

namespace Zer.Services.Company.Impl
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyInfoDataService _companyInfoDataService;

        public CompanyService(ICompanyInfoDataService companyInfoDataService)
        {
            this._companyInfoDataService = companyInfoDataService;
        }

        public List<CompanyInfoDto> GetByLikeName(string likeName)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string companyFullName)
        {
            throw new NotImplementedException();
        }

        public void Add(CompanyInfo model)
        {
            throw new NotImplementedException();
        }

        public bool AddRange(List<CompanyInfo> list)
        {
            throw new NotImplementedException();
        }

        public CompanyInfoDto GetById(int id)
        {
            var entity = _companyInfoDataService.GetById(1);
            return new CompanyInfoDto()
            {
                CompanyName = entity.CompanyName,
                Id = entity.Id,
                TraderRange = entity.TraderRange
            };
        }
        
    }
}
