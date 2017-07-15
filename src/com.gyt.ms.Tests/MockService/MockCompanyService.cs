using System.Collections.Generic;
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
                    new CompanyInfoDto() {CompanyName = "��ݸ�к����������޹�˾", Id = 126, TraderRange = "��װ������"}
                });
        }
    }
}