using System.Web;
using System.Web.Mvc;
using Zer.Framework.Attributes;
using Zer.Framework.Mvc.Logs;
using Zer.Framework.Mvc.Logs.Attributes;

namespace com.gyt.ms
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ValidateLoginAttribute());
            filters.Add(new UserActionLogAttribute());
            filters.Add(new UnValidateLoginAttribute());
            filters.Add(new CustomExceptionAttribute("Error"));
            filters.Add(new HandleErrorAttribute());
        }
    }
}
