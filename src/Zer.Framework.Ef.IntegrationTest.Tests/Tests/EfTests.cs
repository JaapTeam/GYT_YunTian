using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Zer.Framework.Entities;

namespace Zer.Framework.Ef.IntegrationTest.Tests.Tests
{
    [TestFixture]
    public class EfTests
    {
        [SetUp]
        public void InitDb()
        {
            var service = new TestEntityDataService();
            service.Delete(x => x.Id > 5);
        }

        [TearDown]
        public void RoolbackDb()
        {
            var service = new TestEntityDataService();
            service.Delete(x => x.Id > 5);
        }

        [Test]
        public void TestForAdd_TestEntity_Successful()
        {
            var service = new TestEntityDataService();
            var entity = new TestEntity()
            {
                BoolenField = true,
                CreateTime = DateTime.Parse("2017/04/01"),
                State = State.Active,
                StringField = "Test_String_Field_TestForAdd"
            };

            var actual = service.Insert(entity);

            actual.Should().NotBeNull();
            (actual.Id > 0).Should().BeTrue();

            entity.Id = actual.Id;
            actual.ShouldBeEquivalentTo(entity);
        }

        [Test]
        public void TestForQuery_TestEntity_Successful()
        {
            var service = new TestEntityDataService();

            var actual = service.GetAll().Where(x => x.Id <= 5).ToList();

            actual.Should().NotBeNull();
            actual.Should().AllBeOfType<TestEntity>();
            actual.Count.Should().Be(5);
        }

        [Test]
        public void TestForDelete_TestEntity_Successful()
        {
            var service = new TestEntityDataService();

            var entity = new TestEntity()
            {
                BoolenField = true,
                CreateTime = DateTime.Parse("2017/04/01"),
                State = State.Active,
                StringField = "Test_String_Field_ForDelete"
            };

            try
            {
                var addResult = service.Insert(entity);

                service.Delete(addResult);

                var actual = service.GetAll().Any(x => x.StringField == "Test_String_Field_ForDelete");

                actual.Should().BeFalse();
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Test]
        public void TestForUpdate_TestEntity_Successful()
        {
            var service = new TestEntityDataService();

            var entity = new TestEntity()
            {
                BoolenField = true,
                CreateTime = DateTime.Parse("2017/04/01"),
                State = State.Active,
                StringField = "Test_String_Field_ForUpdate"
            };

            try
            {
                var addResult = service.Insert(entity);

                service.Update(addResult.Id, x => x.State = State.SoftDeleted);

                var actual = service.Single(x => x.Id == addResult.Id);

                actual.State.Should().Be(State.SoftDeleted);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
