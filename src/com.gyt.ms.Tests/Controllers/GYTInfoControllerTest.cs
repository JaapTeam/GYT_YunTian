using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using com.gyt.ms.Controllers;
using Castle.Components.DictionaryAdapter;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Zer.AppServices;
using Zer.Entities;
using Zer.GytDto;

namespace com.gyt.ms.Tests.Controllers
{
    [TestFixture]
    public class GYTInfoControllerTest
    {
        [Test]
        public void ExistsgtyGytInfoDtoList_RepeatBidTruckNo_Success()
        {
            // Arrange
            var fakeGytInfoService = new Mock<IGYTInfoService>();
            var fakeCompanyService = new Mock<ICompanyService>();
            var fakeTruckInfoService = new Mock<ITruckInfoService>();
            var fakePeccancyRecrodService = new Mock<IPeccancyRecrodService>();

            fakeGytInfoService.Setup(x => x.GetListByBidTruckNoList(It.IsAny<List<string>>()))
                .Returns(new List<GYTInfoDto>()
                {
                    new GYTInfoDto(){ BidTruckNo = "粤B12345", OriginalTruckNo = "粤O12345", BidCompanyName = "Company6"}
                });

            var controller = new GYTInfoController(fakeGytInfoService.Object,
                                                   fakeCompanyService.Object,
                                                   fakeTruckInfoService.Object,
                                                   fakePeccancyRecrodService.Object);

            var gytInfoDtoList = new List<GYTInfoDto>
            {
                new GYTInfoDto{ BidTruckNo = "粤B12341", OriginalTruckNo = "粤O12341", BidCompanyName = "Company1"},
                new GYTInfoDto{ BidTruckNo = "粤B12342", OriginalTruckNo = "粤O12342", BidCompanyName = "Company2"},
                new GYTInfoDto{ BidTruckNo = "粤B12343", OriginalTruckNo = "粤O12343", BidCompanyName = "Company3"},
                new GYTInfoDto{ BidTruckNo = "粤B12344", OriginalTruckNo = "粤O12344", BidCompanyName = "Company4"},
                new GYTInfoDto{ BidTruckNo = "粤B12344", OriginalTruckNo = "粤O12344", BidCompanyName = "Company5"},
                new GYTInfoDto{ BidTruckNo = "粤B12345", OriginalTruckNo = "粤O12345", BidCompanyName = "Company6"},
                new GYTInfoDto{ BidTruckNo = "粤B12346", OriginalTruckNo = "粤O12346", BidCompanyName = "Company7"}
            };

            // Action
            var actual = controller.ExistsgtyGytInfoDtoList(gytInfoDtoList, out var mustImportgtyGytInfoDtoList);

            // Assert
            actual.Should().NotBeNullOrEmpty();
            actual.Count.Should().Be(1);
            mustImportgtyGytInfoDtoList.Should().NotBeNullOrEmpty();
            mustImportgtyGytInfoDtoList.Select(x => x.BidTruckNo).ToArray()
                .ShouldBeEquivalentTo(new[]
                {
                    "粤B12341",
                    "粤B12342",
                    "粤B12343",
                    "粤B12346"
                });
        }
    }
}
