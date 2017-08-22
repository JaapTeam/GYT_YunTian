using System.Data.Entity;
using Zer.Framework.Entities;
using Zer.Framework.EntityFramework;

namespace Zer.Framework.Ef.IntegrationTest.Tests
{
    public class TestRepositoryBase<TEntity>:EfRepositoryBase<TEntity> where TEntity : class,IEntity<int>
    {
        public TestRepositoryBase() : base(new TestDbContext())
        {
        }
    }

    public abstract class TestRepositoryForGenerateId<TEntity> : EfRepositoryBase<TEntity, string>
        where TEntity : class, IEntity<string>
    {
        public TestRepositoryForGenerateId()
            : base(new TestDbContext())
        {
        }
    }
}