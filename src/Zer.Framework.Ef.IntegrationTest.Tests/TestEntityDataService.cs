using System;
using System.Data.Entity;

namespace Zer.Framework.Ef.IntegrationTest.Tests
{
    public class TestEntityDataService : TestRepositoryBase<TestEntity>
    {
        
    }

    public class TestEntityWithStringPrimaryKeyDataService:TestRepositoryForGenerateId<TestEntityWithStringPrimaryKey>
    {
        public override string GeneratePrimaryKey()
        {
            var id = string.Format("GYT{0:yyyyMMddhhmmssfff}", DateTime.Now);
            return id;
        }
    }
}