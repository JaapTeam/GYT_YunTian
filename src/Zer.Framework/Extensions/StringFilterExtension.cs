using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Zer.Framework.Extensions
{
    public static class StringHelper
    {
        #region String length formatter

        /// <summary>    
        /// 对字符串进行裁剪    
        /// </summary>    
        public static string Trim(this string stringTrim, int maxLength)
        {
            return Trim(stringTrim, maxLength, "...");
        }

        /// <summary>    
        /// 对字符串进行裁剪(区分单字节及双字节字符)    
        /// </summary>    
        /// <param name="rawString">需要裁剪的字符串</param>    
        /// <param name="maxLength">裁剪的长度，按双字节计数</param>    
        /// <param name="appendString">如果进行了裁剪需要附加的字符</param>    
        public static string Trim(this string rawString, int maxLength, string appendString)
        {
            if (string.IsNullOrEmpty(rawString) || rawString.Length <= maxLength)
            {
                return rawString;
            }
            else
            {
                int rawStringLength = Encoding.UTF8.GetBytes(rawString).Length;
                if (rawStringLength <= maxLength * 2)
                    return rawString;
            }

            int appendStringLength = Encoding.UTF8.GetBytes(appendString).Length;
            StringBuilder checkedStringBuilder = new StringBuilder();
            int appendedLenth = 0;
            for (int i = 0; i < rawString.Length; i++)
            {
                char _char = rawString[i];
                checkedStringBuilder.Append(_char);

                appendedLenth += Encoding.Default.GetBytes(new char[] { _char }).Length;

                if (appendedLenth >= maxLength * 2 - appendStringLength)
                    break;
            }

            return checkedStringBuilder.ToString() + appendString;
        }


        #endregion

        #region 特殊字符
        /// <summary>    
        /// 检测是否有Sql危险字符    
        /// </summary>    
        /// <param name="str">要判断字符串</param>    
        /// <returns>判断结果</returns>    
        public static bool IsSafeSqlString(this string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|||  
|  
|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>    
        /// 删除SQL注入特殊字符    
        /// 解然 20070622加入对输入参数sql为Null的判断    
        /// </summary>    
        public static string StripSQLInjection(this string sql)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                //过滤 ' --    
                string pattern1 = @"(\%27)|(\')|(\-\-)";

                //防止执行 ' or    
                string pattern2 = @"((\%27)|(\'))\s*((\%6F)|o|(\%4F))((\%72)|r|(\%52))";

                //防止执行sql server 内部存储过程或扩展存储过程    
                string pattern3 = @"\s+exec(\s|\+)+(s|x)p\w+";

                sql = Regex.Replace(sql, pattern1, string.Empty, RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, pattern2, string.Empty, RegexOptions.IgnoreCase);
                sql = Regex.Replace(sql, pattern3, string.Empty, RegexOptions.IgnoreCase);
            }
            return sql.SQLSafe();
        }

        public static string SQLSafe(this string Parameter)
        {
            Parameter = Parameter.ToLower();
            Parameter = Parameter.Replace("'", "");
            Parameter = Parameter.Replace(">", ">");
            Parameter = Parameter.Replace("<", "<");
            Parameter = Parameter.Replace("\n", "<br>");
            Parameter = Parameter.Replace("\0", "·");
            return Parameter;
        }

        /// <summary>    
        /// 清除xml中的不合法字符    
        /// </summary>    
        /// <remarks>    
        /// 无效字符：    
        /// 0x00 - 0x08    
        /// 0x0b - 0x0c    
        /// 0x0e - 0x1f    
        /// </remarks>    
        public static string CleanInvalidCharsForXML(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            else
            {
                StringBuilder checkedStringBuilder = new StringBuilder();
                Char[] chars = input.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    int charValue = Convert.ToInt32(chars[i]);

                    if ((charValue >= 0x00 && charValue <= 0x08) || (charValue >= 0x0b && charValue <= 0x0c) || (charValue >= 0x0e && charValue <= 0x1f))
                        continue;
                    else
                        checkedStringBuilder.Append(chars[i]);
                }

                return checkedStringBuilder.ToString();

                //string result = checkedStringBuilder.ToString();    
                //result = result.Replace("�", "");    
                //return Regex.Replace(result, @"[\u0000-\u0008\u000B\u000C\u000E-\u001A\uD800-\uDFFF]", delegate(Match m) { int code = (int)m.Value.ToCharArray()[0]; return (code > 9 ? "&#" + code.ToString() : "�" + code.ToString()) + ";"; });    
            }
        }


        /// <summary>    
        /// 改正sql语句中的转义字符    
        /// </summary>    
        public static string mashSQL(this string str)
        {
            return (str == null) ? "" : str.Replace("\'", "'");
        }

        /// <summary>    
        /// 替换sql语句中的有问题符号    
        /// </summary>    
        public static string ChkSQL(this string str)
        {
            return (str == null) ? "" : str.Replace("'", "''");
        }

        /// <summary>    
        ///  判断是否有非法字符    
        /// </summary>    
        /// <param name="strString"></param>    
        /// <returns>返回TRUE表示有非法字符，返回FALSE表示没有非法字符。</returns>    
        public static bool CheckBadStr(this string strString)
        {
            if (string.IsNullOrEmpty(strString)) return false;

            var badStrList = new List<string>
            {
                "-",
                "'",
                ";",
                ":",
                "%",
                "&",
                "#",
                "\"",
                "net user",
                "exec",
                "net localgroup",
                "select",
                "asc",
                "char",
                "mid",
                "insert",
                "order",
                "exec",
                "delete",
                "drop",
                "truncate",
                "xp_cmdshell",
                "<",
                ">"
            };
            var tempStrList = strString.ToLower();

            return badStrList.Any(t => tempStrList.IndexOf(t, StringComparison.Ordinal) != -1);
        }

        #endregion

        #region Tools
        /// <summary>    
        /// 去掉最后一个逗号    
        /// </summary>    
        /// <param name="String">要做处理的字符串</param>    
        /// <returns>去掉最后一个逗号的字符串</returns>    
        public static string DelLastComma(this string String)
        {
            if (String.IndexOf(",") == -1)
            {
                return String;
            }
            return String.Substring(0, String.LastIndexOf(","));
        }

        /// <summary>    
        /// 删除最后一个字符    
        /// </summary>    
        /// <param name="str"></param>    
        /// <returns></returns>    
        public static string ClearLastChar(this string str)
        {
            return (str == "") ? "" : str.Substring(0, str.Length - 1);
        }
        /// <summary>    
        /// html编码    
        /// </summary>    
        /// <param name="chr"></param>    
        /// <returns></returns>    
        public static string html_text(this string chr)
        {
            if (chr == null)
                return "";
            chr = chr.Replace("'", "''");
            chr = chr.Replace("<", "<");
            chr = chr.Replace(">", ">");
            return (chr);
        }
        /// <summary>    
        /// html解码    
        /// </summary>    
        /// <param name="chr"></param>    
        /// <returns></returns>    
        public static string text_html(this string chr)
        {
            if (chr == null)
                return "";
            chr = chr.Replace("<", "<");
            chr = chr.Replace(">", ">");
            return (chr);
        }

        public static bool JustifyStr(this string strValue)
        {
            bool flag = false;
            char[] str = "^<>'=&*, ".ToCharArray(0, 8);
            for (int i = 0; i < 8; i++)
            {
                if (strValue.IndexOf(str[i]) != -1)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }

        public static string CheckOutputString(this string key)
        {
            string OutputString = string.Empty;
            OutputString = key.Replace("<br>", "\n").Replace("<", "<").Replace(">", ">").Replace(" ", " ");
            return OutputString;

        }
        #endregion
    }
}