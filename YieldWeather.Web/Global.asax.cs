using System;

using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using YieldWeather.Web.DI;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace YieldWeather
{
    public class MvcApplication : System.Web.HttpApplication
    {

        private IWindsorContainer _windsorContainer;

        protected void Application_Start()
        {
            BootstrapContainer(); //Castle Windsor DI
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            
        }

        private void BootstrapContainer()
        {
            this._windsorContainer = new WindsorContainer();

            _windsorContainer.Install(new ServiceInstaller());
            _windsorContainer.Install(new ControllerInstaller());



            //_windsorContainer.Install(FromAssembly.This());//all in one go

            var controllerFactory = new WindsorControllerFactory(_windsorContainer.Kernel);

            ControllerBuilder.Current.SetControllerFactory(controllerFactory); ;

        }

        protected void Application_End()
        {
            _windsorContainer.Dispose();
        }
    }
}
