using FluentAssertions;
using NUnit.Framework;
using Zer.Framework.UUID;

namespace Zer.FrameWork.UnitTest.Tests
{
    [TestFixture]
    public class UUIDManagerTest
    {
        [Test]
        public void TestFor_GenerateSingleId_Success()
        {
            var id = UUIdManager.Instance.Queue();
            id.Should().Be(10000);
        }

        [Test]
        public void TestFor_GenerateArrayOfId_Success()
        {
            var idList = UUIdManager.Instance.Queue(5000);
            idList .Should().NotBeNullOrEmpty();
            idList.Count.Should().Be(5000);
        }

    }
}