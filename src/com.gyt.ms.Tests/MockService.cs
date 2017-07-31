using System;
using com.gyt.ms.Tests.MockService;
using Zer.Framework;
using Zer.Services;

namespace com.gyt.ms.Tests
{
    public static class MockService<TService,TEntityDto>
        where TService : class, IAppService
    {
        /// <summary>
        /// 创建Mock服务
        /// </summary>
        public static TService Mock()
        {
            var typeName = typeof(TService).Name.Substring(1);
            string fullTypeName = string.Format("com.gyt.ms.Tests.MockService.Mock{0}", typeName);

            var mockRepository = Activator.CreateInstance(Type.GetType(fullTypeName))
                as MockRepository<TService,TEntityDto>;

            return mockRepository.GetService();
        }
    }
}