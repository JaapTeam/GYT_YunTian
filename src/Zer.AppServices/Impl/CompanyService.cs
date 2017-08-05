using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using AutoMapper;
using Zer.Entities;
using Zer.Framework.Exception;
using Zer.GytDataService;
using Zer.GytDataService.Impl;
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
            return Mapper.Map<CompanyInfoDto>(_companyInfoDataService.GetById(id));
        }

        public List<CompanyInfoDto> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public bool Add(CompanyInfoDto model)
        {
            if (_companyInfoDataService.GetAll().Any(x => x.CompanyName == model.CompanyName))
            {
                throw new CustomException(
                    "公司信息已经存在",
                    new Dictionary<string, string>()
                    {
                        {"CompanyName", model.CompanyName}
                    });
            }

            var companyInfo = Mapper.Map<CompanyInfo>(model);
            return _companyInfoDataService.Insert(companyInfo) != null;
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