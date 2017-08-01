using System.Reflection;
using Castle.MicroKernel.Registration;
using Zer.Framework.Dependency;

namespace Zer.Framework.Module
{
    public abstract class ModuleBase
    {
        private readonly Assembly _currentAssemblys;

        protected ModuleBase(Assembly assembly)
        {
            _currentAssemblys = assembly;
        }

        public virtual void Initial()
        {
            IocManager.Instance.Register(
                Classes.FromAssembly(_currentAssemblys)
                    .IncludeNonPublicTypes()
                    .Where(x => x.Name.EndsWith("Service"))
                    .WithServiceSelf()
                    .WithServiceDefaultInterfaces()
            );
        }
    }
}