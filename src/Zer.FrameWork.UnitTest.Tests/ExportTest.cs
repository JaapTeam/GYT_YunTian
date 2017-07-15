using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Zer.Framework.Export;
using Zer.Framework.Export.Attributes;

namespace Zer.FrameWork.UnitTest.Tests
{
    [TestFixture]
    public class ExportTest
    {
        [Test]
        [NUnit.Framework.Category("Core.Export")]
        public void TestFor_GenerateLineString_AnyStringArray_ReturnCorrectResult()
        {
            // Arrange
            var strArray = new string[] { "Id", "CompanyName", "TraderRange", "CreateData", "ApproverName" };
            var expected = "Id" + "," + "CompanyName" + "," + "TraderRange" + "," + "CreateData" + "," + "ApproverName";

            // Act
            var actual = Export.GenerateLineString(strArray);

            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        [NUnit.Framework.Category("Core.Export")]
        public void TestFor_GenerateLineString_AnyObject_ReturnCorrectResult()
        {
            // Arrange
            var companyInfoDto = new CompanyInfoDto() { CompanyName = "深圳市致远高新科技有限公司", Id = 1, TraderRange = "软件开发与运营", CreateDate = DateTime.Now.AddYears(-1) };
            var expected = "1" + "," + "深圳市致远高新科技有限公司" + "," + "软件开发与运营" + "," + DateTime.Now.AddYears(-1);

            // Act
            var actual = Export.GenerateLineString(companyInfoDto);

            // Assert
            actual.Should().Be(expected);
        }

        [Test]
        [Category("Core.Export")]
        public void TsetFor_GenerateHeaderLineString_InputGenericTypeParameter_ReturnCorrectResult()
        {
            var expected = "编号" + "," + "公司名称" + "," + "经营范围" + "," + "创建日期";

            var actual = Export.GenerateHeaderLineString<CompanyInfoDto>();

            actual.Should().Be(expected);
        }

        [Test]
        [Category("Core.Export")]
        public void TsetFor_GenerateHeaderLineString_InputType_ReturnCorrectResult()
        {
            var expected = "编号" + "," + "公司名称" + "," + "经营范围" + "," + "创建日期";

            var actual = Export.GenerateHeaderLineString(typeof(CompanyInfoDto));

            actual.Should().Be(expected);
        }

        [Test]
        [Category("Core.Export")]
        public void TsetFor_ConvertToMultipleLineString_NormalFlow_ReturnExpectedResult()
        {
            // Arrange
            var inputList = new List<CompanyInfoDto>()
            {
                new CompanyInfoDto(){CompanyName = "深圳一号公司",CreateDate = DateTime.Now,Id=97,TraderRange = "企业信息服务"},
                new CompanyInfoDto(){CompanyName = "深圳二号公司",CreateDate = DateTime.Now,Id=12,TraderRange = "企业信息服务"},
                new CompanyInfoDto(){CompanyName = "深圳三号公司",CreateDate = DateTime.Now,Id=15,TraderRange = "企业信息服务"},
            };

            var expected = new StringBuilder();
            
            expected.AppendLine(Export.GenerateHeaderLineString<CompanyInfoDto>());

            foreach (var obj in inputList)
            {
                var dataLine = Export.GenerateLineString(obj);
                expected.AppendLine(dataLine);
            }

            // Act
            var actual = Export.ConvertToMultipleLineString(inputList);

            // Assert
            actual.ToString().Should().Contain("\n");
            actual.ShouldBeEquivalentTo(expected);
        }


        [Test]
        [Category("Core.Export")]
        public void TsetFor_WriteData_NormalFlow_ReturnCorrectResult()
        {
            // Arrange
            var inputList = new List<CompanyInfoDto>()
            {
                new CompanyInfoDto(){CompanyName = "深圳一号公司",CreateDate = DateTime.Now,Id=97,TraderRange = "企业信息服务"},
                new CompanyInfoDto(){CompanyName = "深圳二号公司",CreateDate = DateTime.Now,Id=12,TraderRange = "企业信息服务"},
                new CompanyInfoDto(){CompanyName = "深圳三号公司",CreateDate = DateTime.Now,Id=15,TraderRange = "企业信息服务"},
            };

            var expected = Export.ConvertToMultipleLineString(inputList).ToString();

            var mockStreamWrite = new Mock<StreamWriter>("AnyPath");
            mockStreamWrite.Setup(x => x.WriteAsync(expected));

            // Act
            Export.WriteData(mockStreamWrite.Object, inputList);

            // Assert
            mockStreamWrite.Verify(x => x.WriteAsync(expected), Times.Exactly(1));
        }

        private class CompanyInfoDto
        {
            [ExportDisplayName("编号")]
            public int Id { get; set; }

            [ExportDisplayName("公司名称")]
            public string CompanyName { get; set; }

            [ExportDisplayName("经营范围")]
            public string TraderRange { get; set; }

            [ExportDisplayName("创建日期")]
            public DateTime CreateDate { get; set; }
        }
    }
}
