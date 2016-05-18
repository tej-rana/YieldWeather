using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Mvc;

namespace YieldWeather.Web.DI
{
    /// <summary>
    /// Tells windsor (our dependecy injection) how to find controllers and
    /// how to install them and describes their lifecycle - e.g per request or singleton
    /// </summary>
    public class ControllerInstaller
    {
        /// <summary>
        /// Registers the interfaces (not classes) for injection into the DI container 
        /// when requested.
        /// </summary>
        /// <param name="container">The Windsor DI container</param>
        /// <param name="store">The configuration store of the kernel</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

            //Register all components
            
            
            //All WEB MVC controllers in this assembly
            //with their lifestyle per request. Creates new thread to serve a new request. 
            //TODO: see if LifestyleSingleton() works better because there is no session objects. i.e the 5 day weather forecast will always be the same for Sydney
            container.Register(
                Classes.FromThisAssembly().BasedOn<IController>().LifestylePerWebRequest()
             );



            /*
            If we were developing an API application we would need to register IHttpController
            //All API controllers
            container.Register(
                Classes.FromThisAssembly().BasedOn<IHttpController>().LifestylePerWebRequest()
             );
             */
        }
    }
}