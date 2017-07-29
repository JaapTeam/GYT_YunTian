using System.Data.Entity;
using Zer.Entities;
using Zer.Framework.EntityFramework;

namespace Zer.GytDataService
{
    public class CompanyInfoDataService : EFRepositoryBase<CompanyInfo, int>
    {
        public CompanyInfoDataService()
            : base(new GytDbContext())
        {
        }
    }
}