using System.Collections.Generic;
using Zer.Services.Company;
using Zer.Services.Company.Dto;

namespace com.gyt.ms.Tests.MockService
{
    public class MockCompanyService : MockRepository<ICompanyService, CompanyInfoDto>
    {
        protected override void SetMock()
        {
            Mock.Setup(x => x.GetByLikeName("天空物流"))
                .Returns(new List<CompanyInfoDto>());

            Mock.Setup(x => x.GetByLikeName("海线物流"))
                .Returns(new List<CompanyInfoDto>()
                {
                    new CompanyInfoDto() {CompanyName = "深圳市海线物流有限公司", Id = 109, TraderRange = "天然气运输"},
                    new CompanyInfoDto() {CompanyName = "东莞市海线物流有限公司", Id = 126, TraderRange = "集装箱运输"}
                });
        }
    }
}