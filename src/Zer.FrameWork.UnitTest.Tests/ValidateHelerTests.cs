using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Zer.Framework.Exception;
using Zer.Framework.Helpers;

namespace Zer.FrameWork.UnitTest.Tests
{
    [TestFixture]
    public class ValidateHelerTests
    {
        [Test]
        public void TestFor_ValidateStringIsSafe_ThrowExpectedException()
        {
            var order = new OrdersForTest { OrderId = "OrderId" };
            order.Customer = new CustomerForTest {CustomerId = "ALFKI"};
            order.Customer.ExtensionInfo = new CustomerExtensionInfoForTest(){Address = "skldfsd@sdf.com--1=1'"};

            try
            {
                ValidateHelper.ValidateObjectIsSafe(order);
            }
            catch (CustomException e)
            {
                e.Message.Should().Contain("参数含有非法字符！");
            }
        }

        [Test]
        public void TestFor_ValidateStringIsSafe_inputList_ThrowExpectedException()
        {
            var order = new OrdersForTest { OrderId = "OrderId" };
            order.Customer = new CustomerForTest { CustomerId = "ALFKI" };
            order.Customer.ExtensionInfo = new CustomerExtensionInfoForTest() { Address = "skldfsd@sdf.com--1=1'" };

            var list = new List<OrdersForTest> {order, order, order};

            try
            {
                ValidateHelper.ValidateObjectIsSafe(list);
            }
            catch (CustomException e)
            {
                e.Message.Should().Contain("参数含有非法字符！");
            }
        }
    }


    public class CustomerForTest
    {
        public string CustomerId { get; set; }

        public CustomerExtensionInfoForTest ExtensionInfo { get; set; }
    }

    public class CustomerExtensionInfoForTest
    {
        public string Address { get; set; }
    }

    public class OrdersForTest
    {
        public string OrderId { get; set; }
        public CustomerForTest Customer { get; set; }
    }
}