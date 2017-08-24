using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Zer.Framework.Helpers;

namespace Zer.Framework.Extensions
{
    public static class StringExtension
    {
        

        public static bool IsNullOrEmpty(this string str)
        {
            return str == null || string.IsNullOrEmpty(str.Trim());
        }
        
        public static string Md5Encoding(this string str)
        {
            return Md5Helper.GetMd5ByString(str);
        }

        public static string Md5Encoding(this string str, Encoding encoding)
        {
            return Md5Helper.GetMd5ByString(str, encoding);
        }

        public static string UrlDecode(this string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        public static string UrlEncode(this string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        public static string HtmlDecode(this string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        public static string HtmlEncode(this string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 非常不安全的类型转换,请慎用
        /// </summary>
        public static T To<T>(this string value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static int ToInt(this string str)
        {
            int val;
            if (!int.TryParse(str, out val)) val = 0;
            return val;
        }

        public static int ToInt(this string str, int def)
        {
            int val;
            if (!int.TryParse(str, out val)) val = def;
            return val;
        }

        public static DateTime ToDateTime(this string start, DateTime defaultValue = default(DateTime))
        {
            DateTime startDate;
            if (!DateTime.TryParse(start, out startDate)) startDate = defaultValue;
            return startDate;
        }

        public static List<string> PartString(this string str, char mark)
        {
            if (str.Length <= 0)
            {
                return new List<string>();
            }

            if (mark.ToString().IsNullOrEmpty())
            {
                return new List<string>();
            }
            var stringList = str.Split(mark).ToList();
            return stringList;
        }
    }
}