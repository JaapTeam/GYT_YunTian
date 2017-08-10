using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Zer.Framework.Exception;
using Zer.Framework.Extensions;

namespace Zer.Framework.Helpers
{
    public static class ValidateHelper
    {

        /// <summary>
        /// 验证输入字符串是否有非法字符
        /// </summary>
        /// <param name="inputStrings"></param>
        public static void ValidataInputString(params string[] inputStrings)
        {
            if (inputStrings.Any(x => x.CheckBadStr()))
            {
                throw new CustomException("参数含有非法字符！", "inputString", string.Join(",", inputStrings));
            }
        }

        /// <summary>
        /// 验证输入字符串是否有非法字符
        /// </summary>
        /// <param name="inputStrings"></param>
        public static void ValidataInputString(params string[][] inputStrings)
        {
            inputStrings.ForEach(ValidataInputString);
        }

        /// <summary>
        /// 验证输入字符串是否有非法字符
        /// </summary>
        /// <param name="inputStrings"></param>
        public static void ValidataInputString(params IEnumerable<string>[] inputStrings)
        {
            inputStrings.Select(x => x.ToArray()).ForEach(ValidataInputString);
        }

        public static void ValidateObjectIsSafe(IList list)
        {
            foreach (var item in list)
            {
                ValidateObjectIsSafe(item);
            }
        }
        
        /// <summary>
        /// 此为递归检查，慎用
        /// </summary>
        /// <param name="obj"></param>
        public static void ValidateObjectIsSafe(object obj)
        {
            var inputString = obj as string;
            if (inputString != null)
            {
                ValidataInputString(inputString);
                return;
            }

            var list = obj as IList;
            if (list != null)
            {
                ValidateObjectIsSafe(list);
                return;
            }
            

            foreach (var property in obj.GetType().GetProperties())
            {
                var value = property.GetValue(obj);

                if (value == null) continue;

                if (value.GetType().IsClass && !(value is string))
                {
                    ValidateObjectIsSafe(value);
                }

                var s = value as string;

                if (s != null)
                {
                    ValidataInputString(s);
                }
            }
        }
    }
}