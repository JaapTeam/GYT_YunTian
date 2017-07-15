using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zer.Framework.Exception
{
    public class CustomException : BaseException
    {
        public CustomException(string msg, Dictionary<string, string> detail)
            :base(GetFormatDetail(msg,detail))
        {
        }

        private static string GetFormatDetail(string msg, Dictionary<string, string> detail)
        {
            foreach (var key in detail.Keys)
            {
                msg += string.Format("\n{0}={1}",key,detail[key]);
            }

            return msg;
        }


        public CustomException(string msg, string filedName, string filedValue)
            : this(msg, new Dictionary<string, string>() { { filedName, filedValue } })
        {
        }
    }
}
