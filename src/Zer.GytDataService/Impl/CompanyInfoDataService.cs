using Zer.Entities;
using Zer.Framework.EntityFramework;

namespace Zer.GytDataService.Impl
{
    public class CompanyInfoDataService : EFRepositoryBase<CompanyInfo, int>,ICompanyInfoDataService
    {
        public CompanyInfoDataService()
            : base(new GytDbContext())
        {
        }

        public override CompanyInfo GetById(int id)
        {
            return new CompanyInfo() {CompanyName = "test companyName", TraderRange = "test for trader range", Id = 1};
        }

    }
}