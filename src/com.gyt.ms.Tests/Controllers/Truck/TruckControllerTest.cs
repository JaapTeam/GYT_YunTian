using System.Collections.Generic;
using com.gyt.ms.Controllers;
using FluentAssertions;
using NUnit.Framework;
using Zer.GytDto;
using Zer.Services;

namespace com.gyt.ms.Tests.Controllers.Truck
{
    [TestFixture]
    public class TruckControllerTest : ControllerTestBase
    {
        [Test]
        [Category("Truck")]
        [Description(@"输入无记录的车牌号码时，返回Fail,提示“未查询到相关数据！”")]
        public void TestForGetTruckInforByTruckN_InputNoRecordTruckNo_ReturnFailAndMsg()
        {
            using (var ctrl = new TruckController(MockService<ITruckInfoService, TruckInfoDto>.Mock()))
            {
                const string truckNo = "粤B12345";
                const string msg = "未查询到相关数据！";

                var actual = ctrl.GetTruckInforByTruckNo(truckNo);

                actual.ShouldBeEquivalentTo(JsonHelper.FailExpected(msg));
            }
        }

        [Test]
        [Category("Truck")]
        [Description(@"输入有记录的车牌号码时，返回该记录")]
        public void TestForGetTruckInforByTruckNo_InputHaveRecordTruckNo_ReturnSuccessAndContent()
        {
            using (var ctrl = new TruckController(MockService<ITruckInfoService, TruckInfoDto>.Mock()))
            {
                var truckNo = "粤B54321";
                var expected = new TruckInfoDto()
                {
                    Id = 1,
                    CompanyId = 109,
                    CompanyName = "深圳市海线物流有限公司",
                    DriverId = 1,
                    DriverName = "张三",
                    FrontTruckNo = "粤B54321" 
                    
                };

                var actual = ctrl.GetTruckInforByTruckNo(truckNo);

                actual.ShouldBeEquivalentTo(JsonHelper.SuccessExpected(expected));
            }
        }

        [Test]
        [Category("Truck")]
        [Description(@"输入无记录的公司编号时，返回Fail,提示“未查询到相关数据！”")]
        public void TestForGetTruckInfoListByCompanyId_InputNoRecordCompantId_ReturnReturnFailAndMsg()
        {
            using (var ctrl = new TruckController(MockService<ITruckInfoService, TruckInfoDto>.Mock()))
            {
                var companyId = 1;
                var expected = "未查询到相关数据！";

                var actual = ctrl.GetTruckInfoListByCompanyId(companyId);

                actual.ShouldBeEquivalentTo(JsonHelper.FailExpected(expected));
            }
        }

        [Test]
        [Category("Truck")]
        [Description(@"输入有记录的公司编号时，返回记录集合")]
        public void TestForGetTruckInfoListByCompanyId_InputHaveRecordCompantId_ReturnSuccessAndContent()
        {
            using (var ctrl = new TruckController(MockService<ITruckInfoService, TruckInfoDto>.Mock()))
            {
                var companyId = 2;
                var expected = new List<TruckInfoDto>()
                {
                    new TruckInfoDto()
                    {
                        Id = 1,
                        CompanyId = 109,
                        CompanyName = "深圳市海线物流有限公司",
                        DriverName = "张三",
                        FrontTruckNo = "粤B54321"
                    },
                    new TruckInfoDto()
                    {
                        Id = 2,
                        CompanyId = 109,
                        CompanyName = "深圳市海线物流有限公司",
                        DriverId = 2,
                        DriverName = "李四",
                        FrontTruckNo = "粤B12345"
                    },
                };

                var actual = ctrl.GetTruckInfoListByCompanyId(companyId);

                actual.ShouldBeEquivalentTo(JsonHelper.SuccessExpected(expected));
            }
        }
    }
}
