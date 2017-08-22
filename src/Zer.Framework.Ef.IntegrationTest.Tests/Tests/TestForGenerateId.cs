using System;
using FluentAssertions;
using NUnit.Framework;
using Zer.Framework.Extensions;

namespace Zer.Framework.Ef.IntegrationTest.Tests.Tests
{
    [TestFixture]
    public class TestForGenerateId
    {
        [Test]
        public void TestFor_GenerateIdAndSaveToDb_Success()
        {
            var testEntity = new TestEntityWithStringPrimaryKey();
            var service = new TestEntityWithStringPrimaryKeyDataService();
            var expectedPartOfId = string.Format("GYT{0:yyyyMMddhh}", DateTime.Now);
            var actual = service.Insert(testEntity);

            actual.Id.IsNullOrEmpty().Should().BeFalse();
            actual.Id.Contains(expectedPartOfId).Should().BeTrue();
        }
    }
}