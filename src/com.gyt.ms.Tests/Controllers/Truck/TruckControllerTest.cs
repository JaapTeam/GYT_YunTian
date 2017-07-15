using System.Collections.Generic;
using com.gyt.ms.Controllers;
using FluentAssertions;
using NUnit.Framework;
using Zer.Services.Truck;
using Zer.Services.Truck.Dto;

namespace com.gyt.ms.Tests.Controllers.Truck
{
    [TestFixture]
    public class TruckControllerTest : ControllerTestBase
    {
        [Test]
        [Category("Truck")]
        [Description(@"输入无记录的车牌号码时，返回空记录")]
        public void TestForExistsByTruckNo_InputNoRecordTruckNo_ReturnSuccessAndEmptyContent()
        {
            using (var ctrl = new TruckController(MockService<ITruckInfoService, TruckInfoDto>.Mock()))
            {
                var truckNo = "粤B12345";
                var expected = new TruckInfoDto();

                var actual = ctrl.GetTruckInforByTruckNo(truckNo);

                actual.ShouldBeEquivalentTo(JsonHelper.SuccessExpected(expected));
            }
        }

        [Test]
        [Category("Truck")]
        [Description(@"输入有记录的车牌号码时，返回该记录")]
        public void TestForExistsByTruckNo_InputHaveRecordTruckNo_ReturnSuccessAndContent()
        {
            using (var ctrl = new TruckController(MockService<ITruckInfoService, TruckInfoDto>.Mock()))
            {
                var truckNo = "粤B54321";
                var expected = new TruckInfoDto()
                {
                    Id = 1, ConpanyName = "天空物流", DriverName = "张三", FrontTruckNo = "粤B54321" 
                    
                };

                var actual = ctrl.GetTruckInforByTruckNo(truckNo);

                actual.ShouldBeEquivalentTo(JsonHelper.SuccessExpected(expected));
            }
        }

        [Test]
        [Category("Truck")]
        [Description(@"输入无记录的公司编号时，返回空记录")]
        public void TestForExistsByTruckNo_InputNoRecordCompantId_ReturnSuccessAndEmptyContent()
        {
            using (var ctrl = new TruckController(MockService<ITruckInfoService, TruckInfoDto>.Mock()))
            {
                var companyId = 1;
                var expected = new List<TruckInfoDto>();

                var actual = ctrl.GetTruckInfoListByCompanyId(companyId);

                actual.ShouldBeEquivalentTo(JsonHelper.SuccessExpected(expected));
            }
        }

        [Test]
        [Category("Truck")]
        [Description(@"输入有记录的公司编号时，返回记录集合")]
        public void TestForExistsByTruckNo_InputHaveRecordCompantId_ReturnSuccessAndContent()
        {
            using (var ctrl = new TruckController(MockService<ITruckInfoService, TruckInfoDto>.Mock()))
            {
                var companyId = 2;
                var expected = new List<TruckInfoDto>()
                {
                    new TruckInfoDto() {Id = 1, ConpanyName = "天空物流", DriverName = "张三", FrontTruckNo = "粤B54321"},
                    new TruckInfoDto() {Id = 2, ConpanyName = "天空物流", DriverName = "李四", FrontTruckNo = "粤B12345"},
                };

                var actual = ctrl.GetTruckInfoListByCompanyId(companyId);

                actual.ShouldBeEquivalentTo(JsonHelper.SuccessExpected(expected));
            }
        }
    }
}
