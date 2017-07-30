using Moq;
using Zer.Framework;
using Zer.Services;

namespace com.gyt.ms.Tests.MockService
{
    public abstract class MockRepository<T, TEntityDto>
        where T : class, IAppService
    {
        protected Mock<T> Mock { get; private set; }

        protected MockRepository()
        {
            Mock = new Mock<T>();
        }

        public T GetService()
        {
            SetMock();
            return Mock.Object;
        }

        protected abstract void SetMock();
    }
}
