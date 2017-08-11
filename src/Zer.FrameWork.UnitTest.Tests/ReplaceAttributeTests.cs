using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Zer.Framework.Attributes;

namespace Zer.FrameWork.UnitTest.Tests
{
    [TestFixture]
    public class ReplaceAttributeTests
    {
        [Test]
        public void TestFor_ValidateStringIsSafe_ThrowExpectedException()
        {
            var order = new OrdersForTest {OrderId = "OrderId"};
            order.Customer = new CustomerForTest {CustomerId = "ALFKI"};
            order.Customer.ExtensionInfo = new CustomerExtensionInfoForTest() {Address = "skldfsd@sdf.com--1=1'"};

            var replaceAttribute = new ReplaceSpecialCharInParameterAttribute("-","_");
            replaceAttribute.ReplaceUnsafeChar(order);

            order.Customer.ExtensionInfo.Address.Should().Be("skldfsd@sdf.com__1=1'");
        }

        [Test]
        public void TestFor_ValidateStringIsSafe_InputList_ThrowExpectedException()
        {
            var order = new OrdersForTest { OrderId = "OrderId" };
            order.Customer = new CustomerForTest { CustomerId = "ALFKI" };
            order.Customer.ExtensionInfo = new CustomerExtensionInfoForTest() { Address = "skldfsd@sdf.com--1=1'" };

            var list = new List<OrdersForTest>(){order,order,order};

            var replaceAttribute = new ReplaceSpecialCharInParameterAttribute("-", "_");
            replaceAttribute.ReplaceUnsafeChar(list);

            list.All(x=>x.Customer.ExtensionInfo.Address == "skldfsd@sdf.com__1=1'").Should().BeTrue();
        }
    }
}