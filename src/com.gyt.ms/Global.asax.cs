using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Zer.Framework.Dependency;
using Zer.GytDto;

namespace com.gyt.ms
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IocConfig.Config();
            AutoMapperConfig.Initialze();
            CacheInitial.ReadConfig();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End()
        {
            IocManager.Instance.IocContainer.Dispose();
        }
    }
}
