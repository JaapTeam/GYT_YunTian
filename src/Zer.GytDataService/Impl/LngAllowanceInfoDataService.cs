using System;
using System.Data.Entity;
using System.Security.Cryptography;
using Zer.Entities;
using Zer.Framework.UUID;

namespace Zer.GytDataService.Impl
{
    public class LngAllowanceInfoDataService : GytRepository<LngAllowanceInfo, string>, ILngAllowanceInfoDataService
    {
        public override string GeneratePrimaryKey()
        {
            return string.Format("LNG{0:yyMMddHHmm}{1}", DateTime.Now, UUIdManager.Instance.Queue());
        }
    }
}