using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zer.Framework.Export.Attributes;

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

            foreach (var propertyInfo in type.GetProperties())
            {
                var displayAttribute = propertyInfo.GetCustomAttribute(typeof(ExportDisplayNameAttribute)) as ExportDisplayNameAttribute;

                if (displayAttribute != null)
                {
                    displayNameList.Add(displayAttribute.DisplayName);
                }

                else
                {
                    displayNameList.Add(propertyInfo.Name);
                }
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
            var values = typeof(T).GetProperties().Select(x => x.GetValue(obj).ToString()).ToArray();
            return GenerateLineString(values);
        }
    }
}
