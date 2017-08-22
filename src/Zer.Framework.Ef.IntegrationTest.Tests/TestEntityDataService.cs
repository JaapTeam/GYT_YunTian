using System;
using System.Data.Entity;
using System.Security.Cryptography;

namespace Zer.Framework.Ef.IntegrationTest.Tests
{
    public class TestEntityDataService : TestRepositoryBase<TestEntity>
    {
        
    }

    public class TestEntityWithStringPrimaryKeyDataService:TestRepositoryForGenerateId<TestEntityWithStringPrimaryKey>
    {
        public override string GeneratePrimaryKey()
        {
            RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider();
            byte[] byteCsp = new byte[10];
            csp.GetBytes(byteCsp);
            var seed = Math.Abs(BitConverter.ToInt32(byteCsp, 0));

            Random r = new Random(seed);
            var value = string.Format("GYT{0:yyMMddhhmsff}{1}", DateTime.Now, r.Next(1000, 9999));
            return value;
        }
    }
}