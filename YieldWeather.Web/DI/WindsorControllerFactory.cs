using System;
using System.Web.Mvc;
using System.Net.Http;
using Castle.Windsor;
using System.Web.Routing;
using System.Web.Http.Dispatcher;
using System.Web.Http.Controllers;
using Castle.MicroKernel;
using System.Web;

namespace YieldWeather.Web.DI
{
    /// <summary>
    /// Factory to instatiate controller instances and release them and inject into
    /// controllers
    /// </summary>
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;

        public WindsorControllerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public override void ReleaseController(IController controller)
        {
            _kernel.ReleaseComponent(controller);
        }


        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
            }
            return (IController)_kernel.Resolve(controllerType);
        }
    }
}