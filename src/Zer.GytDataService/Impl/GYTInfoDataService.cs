using System;
using System.Data.Entity;
using Zer.Entities;

namespace Zer.GytDataService.Impl
{
    public class GYTInfoDataService : GytRepository<GYTInfo,string>, IGYTInfoDataService
    {
        public GYTInfoDataService(DbContext dbContext) : base(dbContext)
        {
        }

        public override string GeneratePrimaryKey()
        {
            var id = string.Format("GYT{0:yyyyMMddhhmmssfff}", DateTime.Now);
            return id;
        }
    }
}
