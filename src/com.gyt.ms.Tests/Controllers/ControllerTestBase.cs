namespace com.gyt.ms.Tests.Controllers
{
    public abstract class ControllerTestBase 
    {
        protected JsonHelper JsonHelper { get; private set; }
        protected ControllerTestBase()
        {
            JsonHelper = new JsonHelper();
        }
    }
}