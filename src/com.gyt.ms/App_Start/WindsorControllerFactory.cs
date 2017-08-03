using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace com.gyt.ms
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, "当前请求的控制器不存在");
            }
            return (IController)_kernel.Resolve(controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            _kernel.ReleaseComponent(controller);
            base.ReleaseController(controller);
        }
    }
}