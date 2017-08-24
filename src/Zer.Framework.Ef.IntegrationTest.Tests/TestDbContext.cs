using System.Data.Entity;

namespace Zer.Framework.Ef.IntegrationTest.Tests
{
    public class TestDbContext : DbContext
    {
        public TestDbContext() : base("TestDbContext")
        {
            
        }

        public DbSet<TestEntity> TestEntities { get; set; }

        public DbSet<TestEntityWithStringPrimaryKey> TestEntityWithStringPrimaryKeys { get; set; }
    }
}