using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Zer.Framework.Dependency
{
    public class IocManager
    {
        public IWindsorContainer IocContainer { get; private set; }

        public static IocManager Instance { get; private set; }

        static IocManager()
        {
            Instance = new IocManager();
        }

        public IocManager()
        {
            IocContainer = new WindsorContainer();
        }

        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        public void Register(params IRegistration[] registrations)
        {

            IocContainer.Register(registrations);
        }
    }
}
