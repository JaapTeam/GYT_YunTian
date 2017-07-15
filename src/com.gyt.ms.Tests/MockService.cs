using System;
using com.gyt.ms.Tests.MockService;
using Zer.Services;

namespace com.gyt.ms.Tests
{
    public static class MockService<T>
        where T : class, IDomainService
    {
        /// <summary>
        /// 创建Mock服务
        /// </summary>
        public static T Mock()
        {
            var typeName = typeof(T).Name.Substring(1);
            string fullTypeName = string.Format("com.gyt.ms.Tests.MockService.Mock{0}", typeName);

            var mockRepository = Activator.CreateInstance(Type.GetType(fullTypeName))
                as MockRepository<T>;

            return mockRepository.GetService();
        }
    }
}