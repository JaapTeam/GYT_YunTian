using NUnit.Framework;
using NUnit.Framework.Internal;
using Zer.GytDto;

namespace com.gyt.ms.Tests
{
    public class TestBase
    {
        [SetUp]
        public void Init()
        {
            AutoMapperConfig.Initialze();
        }
    }
}