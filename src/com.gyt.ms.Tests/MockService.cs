using System;
using com.gyt.ms.Tests.MockService;
using Zer.AppServices;


namespace com.gyt.ms.Tests
{
    public static class MockService<TService,TEntityDto>
        where TService : class, IDomainService<TEntityDto,int>
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