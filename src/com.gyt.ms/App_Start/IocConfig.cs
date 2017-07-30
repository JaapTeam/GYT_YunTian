using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor.Installer;
using Zer.Framework.Dependency;
using Zer.GytDataService;
using Zer.GytDataService.Impl;
using Zer.Services;
using Zer.Services.Impl;

namespace com.gyt.ms
{
    internal static class IocConfig
    {
        public static void Config()
        {
            IocManager.Instance.IocContainer.Install(FromAssembly.This());

            //IocManager.Instance.IocContainer.Register(
            //    Classes.FromThisAssembly()
            //    .IncludeNonPublicTypes()
            //    .BasedOn<IDataService>()
            //    .WithService.Self()
            //    .WithService.DefaultInterfaces()
            //    .LifestyleTransient()
            //);

            IocManager.Instance.Register(
                Component.For<ICompanyInfoDataService, CompanyInfoDataService>()
                    .ImplementedBy<CompanyInfoDataService>()
                    .LifestyleTransient(),
                Component.For<ICompanyService, CompanyService>()
                    .ImplementedBy<CompanyService>()
                    .LifestyleTransient()
            );

            ControllerBuilder.Current.SetControllerFactory(
                new WindsorControllerFactory(IocManager.Instance.IocContainer.Kernel));
        }
    }
}