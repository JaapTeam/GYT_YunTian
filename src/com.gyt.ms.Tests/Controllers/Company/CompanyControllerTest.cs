using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using com.gyt.ms.Controllers;
using FluentAssertions;
using FluentAssertions.Mvc;
using NUnit.Framework;
using Zer.Services.Company;
using Zer.Services.Company.Dto;

namespace com.gyt.ms.Tests.Controllers.Company
{
    [TestFixture]
    public class CompanyControllerTest : ControllerTestBase
    {
        [Test]
        [Category("Company")]
        [Description(@"输入不存在的公司简称时，返回空列表")]
        public void TestForGetCopanyByLikeName_InputNotExistsLikeName_ReturnSuccessAndEmptyContent()
        {
            using (var ctrl = new CompanyController(MockService<ICompanyService>.Mock()))
            {
                var likeName = "天空物流"; 
                var expected = new List<CompanyInfoDto>();

                var actual = ctrl.GetCopanyByLikeName(likeName);

                actual.ShouldBeEquivalentTo(JsonHelper.SuccessExpected(expected));
            }
        }

        [Test]
        [Category("Company")]
        [Description(@"输入存在的公司简称时，返回所有存在公司列表")]
        public void TestForGetCopanyByLikeName_InputExistsLikeName_ReturnSuccessAndEmptyContent()
        {
            using (var ctrl = new CompanyController(MockService<ICompanyService>.Mock()))
            {
                var likeName = "海线物流";

                var expected = new List<CompanyInfoDto>()
                {
                    new CompanyInfoDto() {CompanyName = "深圳市海线物流有限公司", Id = 109, TraderRange = "天然气运输"},
                    new CompanyInfoDto() {CompanyName = "深圳市一维海线物流有限公司", Id = 126, TraderRange = "集装箱运输"}
                };

                var actual = ctrl.GetCopanyByLikeName(likeName);

                actual.ShouldBeEquivalentTo(JsonHelper.SuccessExpected(expected));
            }
        }

        [Test]
        [Category("Company")]
        [Description(@"")]
        public void TestForGetCompanyById_IncorrectId_ReturnNull()
        {
            using (var ctrl = new CompanyController(MockService<ICompanyService>.Mock()))
            {
                var companyId = 112;
                CompanyInfoDto data = null;
                var expected = JsonHelper.SuccessExpected(data);

                var actual = ctrl.GetCompanyById(companyId);

                actual.ShouldBeEquivalentTo(expected);
            }
        }

        [Test]
        [Category("Company")]
        [Description(@"")]
        public void TestForGetCompanyById_CorrectId_ReturnExpectedResult()
        {
            using (var ctrl = new CompanyController(MockService<ICompanyService>.Mock()))
            {
                var companyId = 1;
                CompanyInfoDto data = new CompanyInfoDto() {CompanyName = "深圳市天马物流有限公司", Id = 1, TraderRange = "天然气运输"};
                var expected = JsonHelper.SuccessExpected(data);

                var actual = ctrl.GetCompanyById(companyId);

                actual.ShouldBeEquivalentTo(expected);
            }
        }

        [Test]
        [Category("Company")]
        [Description(@"")]
        public void TestForGetCompanyById_NoInput_ReturnJsonResultWithNull()
        {
            using (var ctrl = new CompanyController(MockService<ICompanyService>.Mock()))
            {
                CompanyInfoDto data = null;
                var expected = JsonHelper.SuccessExpected(data);

                var actual = ctrl.GetCompanyById();

                actual.ShouldBeEquivalentTo(expected);
            }
        }

        [Test]
        [Category("Company")]
        [TestCase("深圳市行空物流有限公司",false,Description = "不存在的公司名，返回false")]
        [TestCase("深圳市天马物流有限公司", true,Description = "存在的公司名，返回 true")]
        public void TestForCompanyIsExists_NotExistsFullName_ReturnJsonWithTrue(string fullName,bool expected)
        {
            using (var ctrl = new CompanyController(MockService<ICompanyService>.Mock()))
            {
                var actual = ctrl.CompanyIsExists(fullName);

                actual.ShouldBeEquivalentTo( JsonHelper.SuccessExpected(expected));
            }
        }

        [Test]
        [Category("Company")]
        public void TestForAddCompany_NormalFlow_ReturnJsonWithTrue()
        {
            using (var ctrl = new CompanyController(MockService<ICompanyService>.Mock()))
            {
                var dto = new CompanyInfoDto() { CompanyName = "深圳市万利物流有限公司" ,TraderRange = "集装箱运输"};

                var actual = ctrl.AddCompany(dto);

                actual.ShouldBeEquivalentTo(JsonHelper.SuccessExpected());
            }
        }

        [Test]
        [Category("Company")]
        public void TestForAddCompany_ExistsCompanyName_ReturnJsonWithFalse()
        {
            using (var ctrl = new CompanyController(MockService<ICompanyService>.Mock()))
            {
                var dto = new CompanyInfoDto() { CompanyName = "深圳市天马物流有限公司", TraderRange = "集装箱运输" };

                var actual = ctrl.AddCompany(dto);

                actual.ShouldBeEquivalentTo(JsonHelper.FailExpected("公司名称已经存在!"));
            }
        }
    }
}
