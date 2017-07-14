using System.Web.Mvc;

namespace com.gyt.ms.Tests.Controllers
{
    public abstract class ControllerTestBase : Controller
    {
        protected ControllerTestBase()
        {
            MockRepository = new MockRepository();
        }

        protected ControllerTestBase(IMockRepository mockRepository)
        {
            MockRepository = mockRepository;
        }
        
        protected IMockRepository MockRepository { get; private set; }
    }
}