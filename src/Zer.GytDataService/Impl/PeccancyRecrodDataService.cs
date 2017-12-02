using System;
using Zer.Entities;
using Zer.Framework.UUID;

namespace Zer.GytDataService.Impl
{
    public class PeccancyRecrodDataService : GytRepository<PeccancyInfo,string>, IPeccancyRecrodDataService
    {
        public override string GeneratePrimaryKey()
        {
            return string.Format("P{0:yyMMddhhmm}{1}", DateTime.Now, UUIdManager.Instance.Queue());
        }
    }
}