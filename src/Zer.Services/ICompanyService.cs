﻿using System.Collections.Generic;
using Zer.Entities;
using Zer.Framework;
using Zer.GytDto;

namespace Zer.Services
{
    public interface ICompanyService : IAppService
    {
        /// <summary>
        /// 模糊查询，输入公司名称关键字，查询包含该字符的公司信息
        /// </summary>
        /// <param name="likeName">公司名称关键字</param>
        /// <returns>an array of <see cref="CompanyInfoDto"/></returns>
        List<CompanyInfoDto> GetByLikeName(string likeName);

        /// <summary>
        /// 判断公司是否存在
        /// </summary>
        /// <param name="companyFullName">完整公司名称</param>
        /// <returns></returns>
        bool Exists(string companyFullName);

        void Add(CompanyInfo model);

        bool AddRange(List<CompanyInfo> list);

        CompanyInfoDto GetById(int id);


    }
}