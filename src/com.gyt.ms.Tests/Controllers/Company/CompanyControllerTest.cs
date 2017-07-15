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
            using (var ctrl = new CompanyController(MockService<ICompanyService, CompanyInfoDto>.Mock()))
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
            using (var ctrl = new CompanyController(MockService<ICompanyService,CompanyInfoDto>.Mock()))
            {
                var likeName = "海线物流";
                var expected = new List<CompanyInfoDto>()
                {
                    new CompanyInfoDto() {CompanyName = "深圳市海线物流有限公司", Id = 109, TraderRange = "天然气运输"},
                    new CompanyInfoDto() {CompanyName = "东莞市海线物流有限公司", Id = 126, TraderRange = "集装箱运输"}
                };

                var actual = ctrl.GetCopanyByLikeName(likeName);

                actual.ShouldBeEquivalentTo(JsonHelper.SuccessExpected(expected));
            }
        }
    }
}
