using System.Web;
using System.Web.Mvc;
using Zer.Framework.Mvc.Logs;
using Zer.Framework.Mvc.Logs.Attributes;

namespace com.gyt.ms
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new UserActionLogAttribute());
            filters.Add(new CustomExceptionAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
