using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Castle.MicroKernel.Registration;
using Zer.Framework.Exception;
using Zer.Framework.Extensions;

namespace Zer.Framework.Attributes
{
    public class ReplaceSpecialCharInParameterAttribute : Attribute
    {
        public string Source { get; private set; }
        public string Target { get; private set; }

        public ReplaceSpecialCharInParameterAttribute(string source, string target)
        {
            Source = source.StripSQLInjection();
            Target = target;
        }

        public IList ReplaceUnsafeChar(IList list)
        {
            var result = Activator.CreateInstance(list.GetType()) as IList;
            if (result == null)
            {
                throw new CustomException("创建类型失败","Type",list.GetType().FullName);
            }
            foreach (var item in list)
            {
                var newItem = ReplaceUnsafeChar(item);
                result.Add(newItem);
            }
            return result;
        }

        public T ReplaceUnsafeChar<T>(T obj)
        {
            if (!typeof(T).IsClass) return obj;
            var strValue = obj as string;
            if (strValue != null)
            {
                var result = (T)Convert.ChangeType(strValue.Replace(Source, Target), typeof(T));
                return result;
            }

            if (obj is IList)
            {
                var list = obj as IList;
                return (T)ReplaceUnsafeChar(list);
            }

            foreach (var propertyInfo in obj.GetType().GetProperties())
            {
                var value = propertyInfo.GetValue(obj);

                if (value == null) return default(T);

                var result = ReplaceUnsafeChar(value);
                propertyInfo.SetValue(obj,result);
            }
            return obj;
        }
    }
}