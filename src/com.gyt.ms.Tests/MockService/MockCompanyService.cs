using System.Collections.Generic;
using Moq;
using Zer.Entities;
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
                    new CompanyInfoDto() {CompanyName = "深圳市一维海线物流有限公司", Id = 126, TraderRange = "集装箱运输"}
                });

            Mock.Setup(x => x.GetById(1))
                .Returns(
                    new CompanyInfoDto() { CompanyName = "深圳市天马物流有限公司", Id = 1, TraderRange = "天然气运输" }
                );

            Mock.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);

            Mock.Setup(x => x.Exists("深圳市天马物流有限公司")).Returns(true);

            //Mock.Setup(x => x.Add(It.IsAny<CompanyInfo>()));

            Mock.Setup(x => x.GetById(It.Is<int>(id => id > 100))).Returns(() => null);
        }
    }
}