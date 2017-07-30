using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
            return Mapper.Map<List<CompanyInfoDto>>(_companyInfoDataService.GetAll()
                .Where(x => x.CompanyName.Contains(likeName)).ToList());
        }

        public bool Exists(string companyFullName)
        {
            return _companyInfoDataService.GetAll().Any(x => x.CompanyName.Equals(companyFullName));
        }

        public void Add(CompanyInfo model)
        {
            _companyInfoDataService.Insert(model);
        }

        public bool AddRange(List<CompanyInfo> list)
        {
            throw new NotImplementedException();
        }

        public CompanyInfoDto GetById(int id)
        {
            var entity = _companyInfoDataService.GetById(1);
            

            return Mapper.Map<CompanyInfoDto>(entity);
        }
        
    }
}
