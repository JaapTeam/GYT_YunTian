using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Zer.Framework;

namespace Zer.GytDataService
{
    public class DataServiceInstall : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .IncludeNonPublicTypes()
                .BasedOn<IDataService>()
                .If(t=>t.Name.EndsWith("DataService"))
                .Configure(c=>c.LifestyleTransient())
            );
        }
    }
}