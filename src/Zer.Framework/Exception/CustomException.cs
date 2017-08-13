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
            msg = string.Format("<h2>{0}</h2>", msg);
            foreach (var key in detail.Keys)
            {
                msg += string.Format("<p>{0}:{1}</p>",key,detail[key]);
            }

            return msg;
        }


        public CustomException(string msg, string filedName, string filedValue)
            : this(msg, new Dictionary<string, string>() { { filedName, filedValue } })
        {
        }
    }
}
