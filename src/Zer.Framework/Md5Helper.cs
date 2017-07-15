using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Zer.Framework
{
    /// <summary>
    /// md5文件加密字符串加密
    /// </summary>
    public static class Md5Helper
    {
        /// <summary>
        /// 提供字符串根据指定的编码获取MD5值
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="encoding">编码格式</param>
        /// <returns>MD5值</returns>
        public static string GetMd5ByString(string str, Encoding encoding)
        {
            MD5 md5 = MD5.Create();
            byte[] bs = encoding.GetBytes(str);
            bs = md5.ComputeHash(bs);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bs.Length; i++)
            {
                sb.Append(bs[i].ToString("x2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 提供字符串获取MD5值(UTF-8编码)
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>MD5值</returns>
        public static string GetMd5ByString(string str)
        {
            return GetMd5ByString(str, Encoding.UTF8);
        }

        /// <summary>
        /// 根据文件获得MD5值
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>MD5值</returns>
        public static string GetMd5ByFile(string filePath)
        {
            //文件不存在
            if (!File.Exists(filePath))
            {
                throw new System.Exception("文件不存在");
            }
            var bs = MD5.Create().ComputeHash(new FileStream(filePath, FileMode.Open));
            var sb = new StringBuilder();
            foreach (var t in bs)
            {
                sb.Append(t.ToString("x2"));
            }
            return sb.ToString();
        }

    }
}
