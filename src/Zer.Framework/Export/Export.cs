using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Zer.Framework.Attributes;
using Zer.Framework.Exception;
using Zer.Framework.Export.Attributes;

namespace Zer.Framework.Export
{
    public class Export
    {
        public static byte[] GetBuffer<T>(IEnumerable<T> data)
            where T : class
        {
            // TODO:Unit Test
            var stringBuilder = ConvertToMultipleLineString(data);
            var contexnt = stringBuilder.ToString();
            return Encoding.Default.GetBytes(contexnt);
        }

        public static void WriteData<T>(StreamWriter streamWriter, IEnumerable<T> data) where T : class
        {
            // TODO:Unit Test
            var stringBuilder = ConvertToMultipleLineString(data);

            //将字符串写入流
            streamWriter.WriteAsync(stringBuilder.ToString());
            streamWriter.Flush();
        }

        public static StringBuilder ConvertToMultipleLineString<T>(IEnumerable<T> data) where T : class
        {
            // 从数据转为字符串
            var stringBuilder = new StringBuilder();

            // 获取首行
            var headerLine = GenerateHeaderLineString<T>();

            stringBuilder.AppendLine(headerLine);

            foreach (T obj in data)
            {
                var dataLine = GenerateLineString(obj);
                stringBuilder.AppendLine(dataLine);
            }
            return stringBuilder;
        }

        public static string GenerateLineString(params string[] columnValues)
        {
            var stringBuilder = new StringBuilder();
            foreach (var str in columnValues)
            {
                stringBuilder.AppendFormat("{0},", str);
            }
            var line = stringBuilder.ToString();
            line = line.Substring(0, line.Length - 1);
            return line;
        }

        public static string GenerateHeaderLineString(Type type)
        {
            var displayNameList = new List<string>();

            var properties = type.GetProperties()
                                 .Where(x => !x.GetCustomAttributes().Any(t => t is ExportIgnoreAttribute))
                                 .OrderBy(x =>
                                     {
                                         var sortAttribute = x.GetCustomAttribute(typeof(ExportSortAttribute)) as ExportSortAttribute;
                                         if (sortAttribute == null)
                                         {
                                             throw new CustomException(
                                                 "缺少属性" + typeof(ExportSortAttribute).FullName,
                                                 "属性名:",
                                                 x.Name
                                             );
                                         }
                                         return sortAttribute.Index;
                                     })
                                 .ToList();

            foreach (var propertyInfo in properties)
            {
                var displayAttribute = propertyInfo.GetCustomAttribute(typeof(ExportDisplayNameAttribute)) as ExportDisplayNameAttribute;

                displayNameList.Add(displayAttribute != null ? displayAttribute.DisplayName : propertyInfo.Name);
            }

            return GenerateLineString(displayNameList.ToArray());
        }

        public static string GenerateHeaderLineString<T>()
        {
            return GenerateHeaderLineString(typeof(T));
        }

        public static string GenerateLineString<T>(T obj)
            where T : class
        {
            var values = typeof(T).GetProperties()
                .Where(x => !x.GetCustomAttributes().Any(t => t is ExportIgnoreAttribute))
                //                .Where(x=>x.GetCustomAttributes())
                .OrderBy(x =>
                    {
                        var sortAttribute = x.GetCustomAttribute(typeof(ExportSortAttribute)) as ExportSortAttribute;
                        if (sortAttribute == null)
                        {
                            throw new CustomException(
                                "缺少属性" + typeof(ExportSortAttribute).FullName,
                                "属性名:",
                                x.Name
                                );
                        }
                        return sortAttribute.Index;
                    })
                .Select(x =>
                {
                    var specilTypeList = new List<Type>
                    {
                        typeof(int),typeof(long),typeof(short)
                    };
                    var value = x.GetValue(obj);
                    if (value == null) return " ";
                    if (value is DateTime) return string.Format("{0:yyyy-MM-dd HH:mm:ss}", value);
                    if (value is Boolean) return (bool) value ? "是" : "否";

                    if (specilTypeList.Contains(value.GetType()))
                    {
                        return string.Format("\"{0}{1}\" ", ((char)(9)).ToString(), value);
                    }

                    return string.Format("\"{0}\" ", value.ToString());

                }).ToArray();
            return GenerateLineString(values);
        }
    }
}
