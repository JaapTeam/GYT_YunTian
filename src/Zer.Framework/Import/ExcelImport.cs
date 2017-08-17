using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.Core.Internal;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Zer.Framework.Attributes;
using Zer.Framework.Dto;
using Zer.Framework.Exception;

namespace Zer.Framework.Import
{
    public class ExcelImport<T> where T : class,IDto, new()
    {
        public ExcelImport(Stream inputExcelStream)
        {
            InputExcelStream = inputExcelStream;
        }

        private Stream InputExcelStream { get; set; }

        public List<T> Read()
        {
            if (InputExcelStream == null)
            {
                throw new CustomException("文件流为空或者上传文件失败，请重试!");
            }

            try
            {
                XSSFWorkbook workbook = new XSSFWorkbook(InputExcelStream);

                var sheet = GetDefaultSheet(workbook);

                return GenerateObjectAndSetValue(sheet);
            }
            catch
            {
                throw new System.Exception("文件内容读取失败，目前仅支持 Excel 2007 及以上版本的数据导入，检查文件格式，并确保文件后辍为 !\".xlsx\" !");
            }
        }

        public List<PropertyInfo> GetProperties()
        {
            // 获取指定类型的所有属性
            var properties = typeof(T).GetProperties().Where(x => x.GetCustomAttribute<ImprotIgnoreAttribute>() == null)
                .ToList();

            if (properties.IsNullOrEmpty())
            {
                throw new CustomException("指定的导入类型不正确，请联系技术支持");
            }

            return properties;
        }

        public List<T> GenerateObjectAndSetValue(ISheet sheet)
        {
            var properties = GetProperties();
            var propertyWithSortIdMap = GetPropertyWithSortIdMap(properties);
            var list = new List<T>();

            var rowReader = sheet.GetRowEnumerator();

            while (rowReader.MoveNext())
            {
                var currentRow = rowReader.Current as IRow;
                if (currentRow == null || currentRow.RowNum == 0) continue;

                list.Add(SetValueWithRow(propertyWithSortIdMap, currentRow));
            }

            return list;
        }

        //获取cell的数据，并设置为对应的数据类型
        public object GetCellValue(ICell cell)
        {
            object value = null;
            try
            {
                if (cell.CellType != CellType.Blank)
                {
                    switch (cell.CellType)
                    {
                        case CellType.Numeric:
                            // Date comes here
                            if (DateUtil.IsCellDateFormatted(cell))
                            {
                                value = cell.DateCellValue;
                            }
                            else
                            {
                                // Numeric type
                                value = cell.NumericCellValue;
                            }
                            break;
                        case CellType.Boolean:
                            // Boolean type
                            value = cell.BooleanCellValue;
                            break;
                        case CellType.Formula:
                            value = cell.CellFormula;
                            break;
                        default:
                            // String type
                            value = cell.StringCellValue;
                            break;
                    }
                }
            }
            catch (System.Exception)
            {
                value = "";
            }

            return value;
        }

        public T SetValueWithRow(Dictionary<int, PropertyInfo> dic, IRow currentRow)
        {
            T t = new T();

            for (int i = 0; i < currentRow.LastCellNum; i++)
            {
                var cell = currentRow.GetCell(i);
                var cellvalue = GetCellValue(cell);
                var property = dic[i];
                property.SetValue(t, ConvertHelper.ChangeType(cellvalue, property.PropertyType));
            }

            //foreach (var index in dic.Keys)
            //{
            //    var propertyInfo = dic[index];

            //    var cellValue = currentRow.Cells[index];

            //    if (cellValue == null)
            //    {
            //        throw new CustomException(string.Format("第{0}行{1}列的值不正确.", currentRow.RowNum + 1, index));
            //    }

            //    propertyInfo.SetValue(t, cellValue.StringCellValue);
            //}

            return t;
        }

        public Dictionary<int, PropertyInfo> GetPropertyWithSortIdMap(List<PropertyInfo> properties)
        {
            Dictionary<int, PropertyInfo> dic = new Dictionary<int, PropertyInfo>();

            foreach (var propertyInfo in properties)
            {
                var sortAttribute = propertyInfo.GetCustomAttribute<ImportSortAttribute>();
                if (sortAttribute == null)
                {
                    throw new CustomException("字段缺少 Sort 属性", "字段名:", propertyInfo.Name);
                }

                dic.Add(sortAttribute.Index - 1, propertyInfo);
            }
            return dic;
        }

        public ISheet GetDefaultSheet(XSSFWorkbook workbook)
        {
            var sheet = workbook.GetSheetAt(0);

            if (sheet == null)
            {
                throw new CustomException("未找到约定的工作薄，请检查文件后重新提交!");
            }
            return sheet;
        }
    }
}