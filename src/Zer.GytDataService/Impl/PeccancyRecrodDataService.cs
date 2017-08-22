using System;
using Zer.Entities;

namespace Zer.GytDataService.Impl
{
    public class PeccancyRecrodDataService : GytRepository<PeccancyInfo,string>, IPeccancyRecrodDataService
    {
        public override string GeneratePrimaryKey()
        {
            return  string.Format("P{0:yyyyMMddhhmmssfff}", DateTime.Now);
        }
    }
}