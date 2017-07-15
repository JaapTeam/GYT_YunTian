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
            Mock.Setup(x => x.GetByLikeName("�������"))
                .Returns(new List<CompanyInfoDto>());

            Mock.Setup(x => x.GetByLikeName("��������"))
                .Returns(new List<CompanyInfoDto>()
                {
                    new CompanyInfoDto() {CompanyName = "�����к����������޹�˾", Id = 109, TraderRange = "��Ȼ������"},
                    new CompanyInfoDto() {CompanyName = "������һά�����������޹�˾", Id = 126, TraderRange = "��װ������"}
                });

            Mock.Setup(x => x.GetById(1))
                .Returns(
                    new CompanyInfoDto() { CompanyName = "�����������������޹�˾", Id = 1, TraderRange = "��Ȼ������" }
                );

            Mock.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);

            Mock.Setup(x => x.Exists("�����������������޹�˾")).Returns(true);

            //Mock.Setup(x => x.Add(It.IsAny<CompanyInfo>()));

            Mock.Setup(x => x.GetById(It.Is<int>(id => id > 100))).Returns(() => null);
        }
    }
}