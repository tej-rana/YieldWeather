using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Facilities.TypedFactory;
using YieldWeather.Services;

namespace YieldWeather.Web.DI
{
    /// <summary>
    /// This class installs all services by resolving the interface to the implemented types.
    /// Please note that there are multiple ways to resolve types. 
    /// For more information check the Windsor documentation.
    /// </summary>
    public class ServiceInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Registers the service interfaces (not classes) for injection into the DI container 
        /// when requested.
        /// </summary>
        /// <param name="container">The Windsor DI container</param>
        /// <param name="store">The configuration store of the kernel</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //first add support to resolve via Typed Factory. There are other ways to resolve. This is just one of them.
            container.AddFacility<TypedFactoryFacility>();

            container.Register(
                Component.For<IService>()
                .ImplementedBy<CurrentWeatherService>()
                .Named("CurrentWeatherService")
                .LifestyleTransient(),
                 Component.For<IService>()
                .ImplementedBy<CurrentWeatherService>()
                .Named("CurrentWeatherService")
                .LifestyleTransient(),
               Component.For<IServiceFactory>().AsFactory()
             );
        }
    }
}