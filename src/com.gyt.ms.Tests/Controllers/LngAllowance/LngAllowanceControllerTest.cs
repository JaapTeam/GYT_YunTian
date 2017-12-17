using System.Collections.Generic;
using com.gyt.ms.Controllers;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Zer.AppServices;
using Zer.AppServices.Impl;
using Zer.GytDataService.Impl;
using Zer.GytDto;

namespace com.gyt.ms.Tests.Controllers.LngAllowance
{
    [TestFixture]
    public class LngAllowanceControllerTest : ControllerTestBase
    {
        [Test]
        public void FilterExistsLngInfoDtoList_EmptyEngineId_ReturnExpectedResult()
        {
            // Arrange
            ILngAllowanceService lngAllowanceService = fakeLngAllowanceService();
            ICompanyService companyService = null;
            ITruckInfoService truckInfoService = null;

            var controller = new LngAllowanceController(lngAllowanceService, companyService, truckInfoService);

            // Action
            List<LngAllowanceInfoDto> lngAllowanceInfoDtoList = new List<LngAllowanceInfoDto>()
            {
                new LngAllowanceInfoDto(){TruckNo = "12340",EngineId = string.Empty,CompanyName = "CompanyName1"},
                new LngAllowanceInfoDto(){TruckNo = "12341",EngineId = string.Empty,CompanyName = "CompanyName2"},
                new LngAllowanceInfoDto(){TruckNo = "12342",EngineId = string.Empty,CompanyName = "CompanyName3"},
                new LngAllowanceInfoDto(){TruckNo = "12343",EngineId = string.Empty,CompanyName = "CompanyName4"},
                new LngAllowanceInfoDto(){TruckNo = "12340",EngineId = string.Empty,CompanyName = "CompanyName5"},
                new LngAllowanceInfoDto(){TruckNo = "12345",EngineId = string.Empty,CompanyName = "CompanyName6"},
                new LngAllowanceInfoDto(){TruckNo = "12346",EngineId = string.Empty,CompanyName = "CompanyName7"},
            };

            var expect = new List<LngAllowanceInfoDto>()
            {
                new LngAllowanceInfoDto(){TruckNo = "12340",EngineId = string.Empty,CompanyName = "CompanyName1"},
                new LngAllowanceInfoDto(){TruckNo = "12340",EngineId = string.Empty,CompanyName = "CompanyName5"},
            };

            var actual = controller.FilterExistsLngInfoDtoList(lngAllowanceInfoDtoList);

            actual.ShouldBeEquivalentTo(expect);
        }

        [Test]
        public void FilterExistsLngInfoDtoList_NormalFlow_ReturnExpectedResult()
        {
            // Arrange
            ILngAllowanceService lngAllowanceService = fakeLngAllowanceService();
            ICompanyService companyService = null;
            ITruckInfoService truckInfoService = null;

            var controller = new LngAllowanceController(lngAllowanceService, companyService, truckInfoService);

            // Action
            List<LngAllowanceInfoDto> lngAllowanceInfoDtoList = new List<LngAllowanceInfoDto>()
            {
                new LngAllowanceInfoDto(){TruckNo = "12340",EngineId = "ABCD",CompanyName = "CompanyName1"},
                new LngAllowanceInfoDto(){TruckNo = "12341",EngineId = "BCDA",CompanyName = "CompanyName2"},
                new LngAllowanceInfoDto(){TruckNo = "12342",EngineId = "CDAB",CompanyName = "CompanyName3"},
                new LngAllowanceInfoDto(){TruckNo = "12343",EngineId = "DABC",CompanyName = "CompanyName4"},
                new LngAllowanceInfoDto(){TruckNo = "12340",EngineId = "",CompanyName = "CompanyName5"},
                new LngAllowanceInfoDto(){TruckNo = "12345",EngineId = "ABCD",CompanyName = "CompanyName6"},
                new LngAllowanceInfoDto(){TruckNo = "12346",EngineId = "ABCD",CompanyName = "CompanyName7"},
            };

            var expect = new List<LngAllowanceInfoDto>()
            {
                new LngAllowanceInfoDto(){TruckNo = "12340",EngineId = "ABCD",CompanyName = "CompanyName1"},
                new LngAllowanceInfoDto(){TruckNo = "12340",EngineId = "",CompanyName = "CompanyName5"},
                new LngAllowanceInfoDto(){TruckNo = "12345",EngineId = "ABCD",CompanyName = "CompanyName6"},
                new LngAllowanceInfoDto(){TruckNo = "12346",EngineId = "ABCD",CompanyName = "CompanyName7"},
            };

            var actual = controller.FilterExistsLngInfoDtoList(lngAllowanceInfoDtoList);

            actual.ShouldBeEquivalentTo(expect);
        }

        [Test]
        public void FilterExistsLngInfoDtoList_EngineIdRelay_ReturnExpectedResult()
        {
            // Arrange
            ILngAllowanceService lngAllowanceService = fakeLngAllowanceService();
            ICompanyService companyService = null;
            ITruckInfoService truckInfoService = null;

            var controller = new LngAllowanceController(lngAllowanceService, companyService, truckInfoService);

            // Action
            List<LngAllowanceInfoDto> lngAllowanceInfoDtoList = new List<LngAllowanceInfoDto>()
            {
                new LngAllowanceInfoDto(){TruckNo = "12340",EngineId = "ABCD",CompanyName = "CompanyName1"},
                new LngAllowanceInfoDto(){TruckNo = "12341",EngineId = "BCDA",CompanyName = "CompanyName2"},
                new LngAllowanceInfoDto(){TruckNo = "12342",EngineId = "CDAB",CompanyName = "CompanyName3"},
                new LngAllowanceInfoDto(){TruckNo = "12343",EngineId = "DABC",CompanyName = "CompanyName4"},
                new LngAllowanceInfoDto(){TruckNo = "12344",EngineId = "",CompanyName = "CompanyName5"},
                new LngAllowanceInfoDto(){TruckNo = "12345",EngineId = "ABCD",CompanyName = "CompanyName6"},
                new LngAllowanceInfoDto(){TruckNo = "12346",EngineId = "ABCD",CompanyName = "CompanyName7"},
            };

            var expect = new List<LngAllowanceInfoDto>()
            {
                new LngAllowanceInfoDto(){TruckNo = "12340",EngineId = "ABCD",CompanyName = "CompanyName1"},
                new LngAllowanceInfoDto(){TruckNo = "12345",EngineId = "ABCD",CompanyName = "CompanyName6"},
                new LngAllowanceInfoDto(){TruckNo = "12346",EngineId = "ABCD",CompanyName = "CompanyName7"},
            };

            var actual = controller.FilterExistsLngInfoDtoList(lngAllowanceInfoDtoList);

            actual.ShouldBeEquivalentTo(expect);
        }

        private ILngAllowanceService fakeLngAllowanceService()
        {
            //var existsList = new List<LngAllowanceInfoDto>()
            //{
            //    new LngAllowanceInfoDto {TruckNo = "12340",EngineId = string.Empty,CompanyName = "CompanyName1"},
            //    new LngAllowanceInfoDto {TruckNo = "12341",EngineId = string.Empty,CompanyName = "CompanyName2"},
            //    new LngAllowanceInfoDto {TruckNo = "12342",EngineId = string.Empty,CompanyName = "CompanyName3"},
            //}.ToArray();

            var mockService = new Mock<ILngAllowanceService>();
            mockService.Setup(x => x.Exists(It.IsAny<LngAllowanceInfoDto>())).Returns(true);
            return mockService.Object;
        }
    }
}