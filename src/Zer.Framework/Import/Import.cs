using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Zer.Framework.Attributes;
using Zer.Framework.Exception;
using Zer.Framework.Extensions;

namespace Zer.Framework.Import
{
    public class Import
    {
        public static List<T> Read<T>(StreamReader reader, int startIndex = 0)
            where T : class ,new()
        {
            var list = new List<T>();

            string currentLine;

            var lineIndex = -1;
            while (!(currentLine = reader.ReadLine()).IsNullOrEmpty())
            {
                lineIndex++;
                if (lineIndex < startIndex) continue;

                var obj = ConvertTo<T>(currentLine);
                list.Add(obj);
            }

            return list;
        }

        public static T ConvertTo<T>(string formatString)
            where T : class ,new()
        {
            var list = formatString.Split(',');
            var properties = typeof(T).GetProperties()
                .Where(x =>
                {
                    var ignore = x.GetCustomAttribute<ImprotIgnoreAttribute>();
                    return ignore == null;
                }).ToArray();

            properties = properties.Select(x =>
            {
                var sortAttribute = x.GetCustomAttribute(typeof(SortAttribute)) as SortAttribute;
                if (sortAttribute == null)
                {
                    throw new CustomException("缺少属性" + typeof(SortAttribute).FullName, "属性名:", x.Name);
                }
                return new { Key = sortAttribute.Index, Value = x };
            }).OrderBy(x => x.Key).Select(x => x.Value).ToArray();

            if (list.Count() != properties.Count())
            {
                throw new CustomException("格式错误,输入数据列总数不匹配", new Dictionary<string, string>
                {
                    {"输入字符串内容",formatString},
                    {"输入字符串列数",list.Length.ToString()},
                    {typeof(T).FullName,properties.Length.ToString()}
                });
            }

            var obj = new T();

            for (int i = 0; i < list.Length; i++)
            {
                properties[i].SetValue(obj, Convert.ChangeType(list[i], properties[i].PropertyType));
            }

            return obj;
        }
    }
}
