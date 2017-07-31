using Autofac;

namespace Zer.Framework.Ioc
{
    public static class DiConfig
    {
        private static IContainer _container;

        public static IContainer RegisterServices(ContainerBuilder builder)
        {
            _container = builder.Build();//返回容器
            return _container;
        }

        /// <summary>
        /// 获取当前Resolve实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}