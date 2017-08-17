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
                throw new CustomException("�ļ���Ϊ�ջ����ϴ��ļ�ʧ�ܣ�������!");
            }

            try
            {
                XSSFWorkbook workbook = new XSSFWorkbook(InputExcelStream);

                var sheet = GetDefaultSheet(workbook);

                return GenerateObjectAndSetValue(sheet);
            }
            catch
            {
                throw new System.Exception("�ļ����ݶ�ȡʧ�ܣ�Ŀǰ��֧�� Excel 2007 �����ϰ汾�����ݵ��룬����ļ���ʽ����ȷ���ļ����Ϊ !\".xlsx\" !");
            }
        }

        public List<PropertyInfo> GetProperties()
        {
            // ��ȡָ�����͵���������
            var properties = typeof(T).GetProperties().Where(x => x.GetCustomAttribute<ImprotIgnoreAttribute>() == null)
                .ToList();

            if (properties.IsNullOrEmpty())
            {
                throw new CustomException("ָ���ĵ������Ͳ���ȷ������ϵ����֧��");
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

        //��ȡcell�����ݣ�������Ϊ��Ӧ����������
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
            //        throw new CustomException(string.Format("��{0}��{1}�е�ֵ����ȷ.", currentRow.RowNum + 1, index));
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
                    throw new CustomException("�ֶ�ȱ�� Sort ����", "�ֶ���:", propertyInfo.Name);
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
                throw new CustomException("δ�ҵ�Լ���Ĺ������������ļ��������ύ!");
            }
            return sheet;
        }
    }
}