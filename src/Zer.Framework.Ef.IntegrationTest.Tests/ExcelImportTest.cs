using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using FluentAssertions;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NUnit.Framework;
using Zer.Framework.Attributes;
using Zer.Framework.Import;
using Zer.GytDto;

namespace Zer.Framework.Ef.IntegrationTest.Tests
{
    [TestFixture]
    public class ExcelImportTest
    {
        [Test]
        public void TestFor_GetProperties_Success()
        {
            var excelImport = new ExcelImport<LngAllowanceInfoDto>(null);

            var expected = new string[]
            {
                "CompanyName",
                "LotId",
                "TruckNo",
                "EngineId",
                "CylinderDefaultId",
                "CylinderSeconedId"
            };

            var actual = excelImport.GetProperties();

            actual.Select(x => x.Name).ToArray().ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void TestFor_GetDefaultSheetWithExcel2010_Success()
        {
            using (var stream = new FileStream(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\AssertFile\\lng.xlsx", FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook workbook = new XSSFWorkbook(stream);
                var excelImport = new ExcelImport<LngAllowanceInfoDto>(stream);

                var expected = workbook.GetSheetAt(0);

                var actual = excelImport.GetDefaultSheet(workbook);

                actual.ShouldBeEquivalentTo(expected);
            }
        }

        [Test]
        public void TestFor_SetValueWithRow_Success()
        {
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\AssertFile\\lng.xlsx";
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                XSSFWorkbook workbook = new XSSFWorkbook(stream);
                var excelImport = new ExcelImport<LngAllowanceInfoDto>(stream);
                var sheet = excelImport.GetDefaultSheet(workbook);

                var expected = new LngAllowanceInfoDto
                {
                    CompanyName = "深圳市通谦物流有限公司",
                    LotId = 1,
                    TruckNo = "粤BX4262",
                    EngineId = "",
                    CylinderDefaultId = "CP450C-13165-82",
                    CylinderSeconedId = "14P500-324-03"
                   
                };

                List<string> message = new List<string>();

                var actual = excelImport.SetValueWithRow(excelImport.GetPropertyWithSortIdMap(excelImport.GetProperties()),
                    sheet.GetRow(1), message);

                message.Should().BeEmpty();
                actual.ShouldBeEquivalentTo(expected);
            }
        }

        [Test]
        public void TestFor_Read_Success()
        {
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\AssertFile\\lng.xlsx";
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var excelImport = new ExcelImport<LngAllowanceInfoDto>(stream);
                var actual = excelImport.Read();

                actual.Should().AllBeOfType<LngAllowanceInfoDto>();
                actual.Count.Should().Be(17);
            }
        }

        //[Test]
        //public void TestFor_GetDefaultSheet_WithExcel2007_Success()
        //{
        //    using (var stream = new FileStream(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\AssertFile\\lng.xls", FileMode.Open, FileAccess.Read))
        //    {
        //        HSSFWorkbook workbook = new HSSFWorkbook(stream);
        //        var excelImport = new ExcelImport<LngAllowanceInfoDto>();

        //        var expected = workbook.GetSheetAt(0);

        //        var actual = excelImport.GetDefaultSheet(workbook);

        //        actual.ShouldBeEquivalentTo(expected);
        //    }
        //}
    }
}