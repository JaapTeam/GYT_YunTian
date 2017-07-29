using System;
using System.Collections.Generic;
using Zer.Entities;
using Zer.GytDataService;
using Zer.Services.Company.Dto;

namespace Zer.Services.Company.Impl
{
    public class CompanyService : ICompanyService
    {
        private readonly CompanyInfoDataService companyInfoDataService;

        public CompanyService(CompanyInfoDataService companyInfoDataService)
        {
            this.companyInfoDataService = companyInfoDataService;
        }

        public CompanyInfoDto GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<CompanyInfoDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Add(CompanyInfoDto model)
        {
            throw new NotImplementedException();
        }

        public bool AddRange(List<CompanyInfoDto> list)
        {
            throw new NotImplementedException();
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
    }
}
