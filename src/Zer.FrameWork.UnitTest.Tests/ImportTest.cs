using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using Zer.Framework.Attributes;
using Zer.Framework.Export.Attributes;
using Zer.Framework.Extensions;

namespace Zer.Framework.UnitTest.Tests
{
    [TestFixture()]
    public class ImportTest
    {
        [Test]
        [Category("Core.Import")]
        public void TestFor_ConvertTo_NormalFlow_ReturnExpectedResult()
        {
            var inputString = "1" + "," + "软件开发与运营" + "," + "深圳市致远高新科技有限公司" + "," + "2017-7-12";

            var expected = new CompanyInfoDto()
            {
                CompanyName = "深圳市致远高新科技有限公司",
                CreateDate = "2017-7-12".ToDateTime(),
                Id = 1,
                TraderRange = "软件开发与运营"
            };

            var actual = Import.Import.ConvertTo<CompanyInfoDto>(inputString);

            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [Category("Core.Import")]
        public void TestFor_Read_WithStartIndex_ReturnExpected()
        {
            // Arrange
            var stringBuilder = new StringBuilder();
            var expected = new List<CompanyInfoDto>();
            var startIndex = 5;

            for (int i = 0; i < 10; i++)
            {
               
                var dto = new CompanyInfoDto()
                {
                    CompanyName = string.Format("深圳市{0}号公司", i),
                    CreateDate = string.Format("{0:yy-MM-dd}", DateTime.Now.AddDays(-i)).ToDateTime(),
                    Id = i,
                    TraderRange = string.Format("经营范围{0}", i)
                };
                stringBuilder.AppendLine(dto.ToString());

                if (i >= 5)
                {
                    expected.Add(dto);
                }
            }

            var readString = stringBuilder.ToString();
            var data = Encoding.UTF8.GetBytes(readString);

            var stream = new MemoryStream(data);

            var streamReader = new StreamReader(stream);

            // Act
            var actual = Import.Import.Read<CompanyInfoDto>(streamReader,startIndex);

            // Assert
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test]
        [Category("Core.Import")]
        public void TestFor_Read_NormalFlow_ReturnExpected()
        {
            // Arrange
            var stringBuilder = new StringBuilder();
            var expected = new List<CompanyInfoDto>();

            for (int i = 0; i < 10; i++)
            {
                var dto = new CompanyInfoDto()
                {
                    CompanyName = string.Format("深圳市{0}号公司",i),
                    CreateDate = string.Format("{0:yy-MM-dd}",DateTime.Now.AddDays(-i)).ToDateTime(),
                    Id = i,
                    TraderRange = string.Format("经营范围{0}",i)
                };
                stringBuilder.AppendLine(dto.ToString());

                expected.Add(dto);
            }

            var readString = stringBuilder.ToString();
            var data = Encoding.UTF8.GetBytes(readString);

            var stream = new MemoryStream(data);

            var streamReader = new StreamReader(stream);

            // Act
            var actual = Import.Import.Read<CompanyInfoDto>(streamReader);

            // Assert
            actual.ShouldBeEquivalentTo(expected);
        }

        private class CompanyInfoDto
        {
            [Sort(1)]
            public int Id { get; set; }

            [Sort(3)]
            public string CompanyName { get; set; }

            [Sort(2)]
            public string TraderRange { get; set; }

            [Sort(4)]
            public DateTime CreateDate { get; set; }

            public override string ToString()
            {
                return string.Format(
                                    "{0},{1},{2},{3:yyyy-M-d dddd}",
                                    Id,
                                    TraderRange,
                                    CompanyName,
                                    CreateDate);
            }
        }
    }
}
