using System.IO;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Zer.Framework.Dependency;
using Zer.Framework.Logger;
using Zer.Framework.Mvc.Logs;
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

            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
            Log4NetLogger.Logger.Info("网站启动服务");
        }

        protected void Application_End()
        {
            Log4NetLogger.Logger.Info("网站停止服务");
            IocManager.Instance.IocContainer.Dispose();
        }
    }
}
