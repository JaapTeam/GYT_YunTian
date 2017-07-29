using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Installer;
using Zer.Framework;
using Zer.Framework.Dependency;
using Zer.GytDataService;
using Zer.GytDataService.Impl;
using Zer.Services.Company;
using Zer.Services.Company.Impl;

namespace com.gyt.ms
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IocManager.Instance.IocContainer.Install(FromAssembly.This());
            
            IocManager.Instance.Register(
                Component.For<ICompanyInfoDataService, CompanyInfoDataService>()
                         .ImplementedBy<CompanyInfoDataService>()
                         .LifestyleTransient()
                         ,
                Component.For<ICompanyService, CompanyService>()
                         .ImplementedBy<CompanyService>()
                         .LifestyleTransient()
                );

            ControllerBuilder.Current.SetControllerFactory(
                new WindsorControllerFactory(IocManager.Instance.IocContainer.Kernel));
        }

        protected void Application_End()
        {
            IocManager.Instance.IocContainer.Dispose();
        }
    }
}
