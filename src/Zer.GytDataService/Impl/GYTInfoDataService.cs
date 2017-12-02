using System;
using System.Data.Entity;
using System.Security.Cryptography;
using Zer.Entities;
using Zer.Framework.UUID;

namespace Zer.GytDataService.Impl
{
    public class GYTInfoDataService : GytRepository<GYTInfo, string>, IGYTInfoDataService
    {
        public override string GeneratePrimaryKey()
        {
            return string.Format("GYT{0:yyMMddhhmm}{1}", DateTime.Now, UUIdManager.Instance.Queue());
        }
    }
}
