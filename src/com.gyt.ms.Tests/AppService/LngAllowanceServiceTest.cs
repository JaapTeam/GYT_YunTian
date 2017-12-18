using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Zer.AppServices.Impl;
using Zer.Entities;
using Zer.GytDataService;
using Zer.GytDto;

namespace com.gyt.ms.Tests.AppService
{
    [TestFixture]
    public class LngAllowanceServiceTest : TestBase
    {
        [Test]
        public void Exists_TruckNoExists_EngineIdisEmpty_ReturnTrue()
        {
            ILngAllowanceInfoDataService lngAllowanceInfoDataService = FakeILngAllowanceInfoDataService();
            var service = new LngAllowanceService(lngAllowanceInfoDataService);

            LngAllowanceInfoDto lngAllowanceInfoDto = new LngAllowanceInfoDto()
            {
                EngineId = string.Empty,
                TruckNo =  "ABC"
            };
            var actual = service.Exists(lngAllowanceInfoDto);

            actual.Should().BeTrue();
        }

        [Test]
        public void Exists_TruckNoNotExists_ReturnTrue()
        {
            ILngAllowanceInfoDataService lngAllowanceInfoDataService = FakeILngAllowanceInfoDataService();
            var service = new LngAllowanceService(lngAllowanceInfoDataService);

            LngAllowanceInfoDto lngAllowanceInfoDto = new LngAllowanceInfoDto()
            {
                EngineId = string.Empty,
                TruckNo = "XXX"
            };
            var actual = service.Exists(lngAllowanceInfoDto);

            actual.Should().BeFalse();
        }

        [Test]
        public void Exists_EngineIDExists_ReturnTrue()
        {
            ILngAllowanceInfoDataService lngAllowanceInfoDataService = FakeILngAllowanceInfoDataService();
            var service = new LngAllowanceService(lngAllowanceInfoDataService);

            LngAllowanceInfoDto lngAllowanceInfoDto = new LngAllowanceInfoDto()
            {
                EngineId = "333",
                TruckNo = "XXX"
            };
            var actual = service.Exists(lngAllowanceInfoDto);

            actual.Should().BeTrue();
        }

        [Test]
        public void Exists_TruckNoExists_ReturnTrue()
        {
            ILngAllowanceInfoDataService lngAllowanceInfoDataService = FakeILngAllowanceInfoDataService();
            var service = new LngAllowanceService(lngAllowanceInfoDataService);

            LngAllowanceInfoDto lngAllowanceInfoDto = new LngAllowanceInfoDto()
            {
                EngineId = "",
                TruckNo = "TYU"
            };
            var actual = service.Exists(lngAllowanceInfoDto);

            actual.Should().BeTrue();
        }

        private ILngAllowanceInfoDataService FakeILngAllowanceInfoDataService()
        {
            var list = new List<LngAllowanceInfo>
            {
                new LngAllowanceInfo(){TruckNo = "ABC",EngineId = ""},
                new LngAllowanceInfo(){TruckNo = "BCD",EngineId = "333"},
                new LngAllowanceInfo(){TruckNo = "XYZ",EngineId = "111"},
                new LngAllowanceInfo(){TruckNo = "TYU",EngineId = "222"},
            }.AsQueryable();

            var mock = new Mock<ILngAllowanceInfoDataService>();
            mock.Setup(x => x.GetAll()).Returns(list);

            return mock.Object;
        }
    }
}