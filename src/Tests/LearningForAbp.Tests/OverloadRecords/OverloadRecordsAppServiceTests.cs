using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningForAbp.IRepositories;
using LearningForAbp.OverloadRecords;
using LearningForAbp.OverloadRecords.Dtos;
using Moq;
using Shouldly;
using Xunit;

namespace LearningForAbp.Tests.OverloadRecords
{
    public class OverloadRecordsAppServiceTests : LearningForAbpTestBase
    {
        [Fact]
        public void TestFor_GetAll_NormalFlow_ReturnCorrectResult()
        {
            // Arrange
            var mock_Repository = MockRepository();

            var overloadRecordsAppService = new OverloadRecordsAppService(mock_Repository.Object);

            // Act
            var actual = overloadRecordsAppService.GetAll();

            // Assert
            actual.ShouldNotBeNull();
            actual.ShouldBeOfType<GetOverloadRecordsOutput>();
        }

        [Fact]
        public void TestFor_GetById_CorrectInput_ReturnCorrectResult()
        {
            // Arrange
            var mock_Repository = MockRepository();
            var input = new SearchOverloadRecordInput() { Id = 1 };
            var overloadRecordsAppService = new OverloadRecordsAppService(mock_Repository.Object);
            var expected = new OverloadRecordDto
            {
                Id = 1,
                TruckNo = "A10009"
            };

            // Act
            var actual = overloadRecordsAppService.GetById(input);

            // Assert
            actual.ShouldNotBeNull();
            actual.ShouldBeOfType<OverloadRecordDto>();
            actual.Id.ShouldBe(1);
            actual.TruckNo.ShouldBe("A10009");
        }

        [Fact]
        public void TestFor_Add_AnyCorrectInput_SaveSuccess_And_ReturnCorrectResult()
        {
            // Arrange
            var mock = new Mock<IOverloadRecordsRepository>();
            mock.Setup(x => x.Add(It.IsAny<LearningForAbp.OverloadRecords.OverloadRecords>()))
                .Callback(new Action<LearningForAbp.OverloadRecords.OverloadRecords>(records =>
                {
                    // Assert
                    records.TruckNo.ShouldBe("A10009");
                    records.AxleQuantity.ShouldBe(8);
                }));

            var overloadRecordsAppService = new OverloadRecordsAppService(mock.Object);
            
            // Action
            overloadRecordsAppService.Add(new AddOverloadRecordInput()
            {
                TruckNo = "A10009",
                AxleQuantity = 8
            });
        }



        private Mock<IOverloadRecordsRepository> MockRepository()
        {
            var mockRepository = new Mock<IOverloadRecordsRepository>();
            mockRepository
                .Setup(x => x.GetAll())
                .Returns(() =>
                    new List<LearningForAbp.OverloadRecords.OverloadRecords>()
                    {
                        new LearningForAbp.OverloadRecords.OverloadRecords() {Id = 1, TruckNo = "A10009"},
                        new LearningForAbp.OverloadRecords.OverloadRecords() {Id = 2, TruckNo = "A10007"},
                        new LearningForAbp.OverloadRecords.OverloadRecords() {Id = 3, TruckNo = "A10008"},
                        new LearningForAbp.OverloadRecords.OverloadRecords() {Id = 4, TruckNo = "A10006"}
                    }.AsQueryable()
                );


            mockRepository.Setup(x => x.Add(It.IsNotNull<LearningForAbp.OverloadRecords.OverloadRecords>()))
                .Callback(new Action<LearningForAbp.OverloadRecords.OverloadRecords>(x =>
                {
                    x.TruckNo.ShouldBe("A10008");
                }));
            return mockRepository;
        }
    }
}
