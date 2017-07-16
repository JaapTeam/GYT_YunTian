using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Zer.Framework.Attributes;
using Zer.Framework.Exception;
using Zer.Framework.Export.Attributes;
using System.Web.Mvc;

namespace Zer.Framework.Export
{
    public class Export
    {
        public static void WriteData<T>(StreamWriter streamWriter, IEnumerable<T> data) where T : class
        {
            var stringBuilder = ConvertToMultipleLineString(data);

            //将字符串写入流
            streamWriter.WriteAsync(stringBuilder.ToString());
            streamWriter.Flush();
        }

        public static byte[] GetBuffer<T>(IEnumerable<T> data)
            where T : class
        {
            var stringBuilder = ConvertToMultipleLineString(data);
            var contexnt = stringBuilder.ToString();
            return Encoding.Default.GetBytes(contexnt);
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
                                 .OrderBy(x =>
                                     {
                                         var sortAttribute = x.GetCustomAttribute(typeof(SortAttribute)) as SortAttribute;
                                         if (sortAttribute == null)
                                         {
                                             throw new CustomException(
                                                 "缺少属性" + typeof(SortAttribute).FullName,
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
                .Where(x => x.GetCustomAttribute(typeof(IgnoreAttribute)) == null)
                .OrderBy(x =>
                    {
                        var sortAttribute = x.GetCustomAttribute(typeof(SortAttribute)) as SortAttribute;
                        if (sortAttribute == null)
                        {
                            throw new CustomException(
                                "缺少属性" + typeof(SortAttribute).FullName,
                                "属性名:",
                                x.Name
                                );
                        }
                        return sortAttribute.Index;
                    })
                .Select(x => x.GetValue(obj).ToString()).ToArray();
            return GenerateLineString(values);
        }
    }
}
