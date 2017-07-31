using Zer.GytDto;

namespace com.gyt.ms.Tests.Controllers
{
    public abstract class ControllerTestBase 
    {
        static ControllerTestBase()
        {
            AutoMapperConfig.Initialze();
        }

        protected JsonHelper JsonHelper { get; private set; }
        protected ControllerTestBase()
        {
            JsonHelper = new JsonHelper();
        }
    }
}