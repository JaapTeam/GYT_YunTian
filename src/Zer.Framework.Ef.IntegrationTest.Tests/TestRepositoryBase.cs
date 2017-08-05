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
}