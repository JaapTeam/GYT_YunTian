using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Installer;
using Zer.Framework.Dependency;
using Zer.Framework.Mvc.Logs;
using Zer.GytDataService;
using Zer.GytDataService.Impl;
using Zer.Services;

namespace com.gyt.ms
{
    internal static class IocConfig
    {
        public static void Config()
        {
            IocManager.Instance.IocContainer.Install(FromAssembly.This());

            new DataServiceModule().Initial();
            new ApplicationServiceModule().Initial();
            new MvcLoggerModule().Initial();
            
            ControllerBuilder.Current.SetControllerFactory(
                new WindsorControllerFactory(IocManager.Instance.IocContainer.Kernel));
        }
    }
}