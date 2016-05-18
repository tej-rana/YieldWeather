using System;
using System.Web.Mvc;
using System.Net.Http;
using Castle.Windsor;

namespace YieldWeather.Web.DI
{
    /// <summary>
    /// This class is used to create and dispose controller instances
    /// </summary>
    public class WindsorControllerActivator
    {
        private readonly IWindsorContainer _container;

        /// <summary>
        /// Create a new instance of WindsorControllerActivator and assign the instance of IContainer to it
        /// </summary>
        /// <param name="container"></param>
        public WindsorControllerActivator(IWindsorContainer container)
        {
            this._container = container;
        }


        /// <summary>
        /// Create a new instance of Controller
        /// </summary>
        /// <param name="request">The Http Request Message Object</param>
        /// <param name="controllerDescriptor">The Controller Descriptor that describes the controller object</param>
        /// <param name="controllerType">The Type of the controller</param>
        /// <returns></returns>
        public IController Create(HttpRequestMessage request, ControllerDescriptor controllerDescriptor, Type controllerType)
        {
            //find the api controller to match this
            var controller =
             (IController)this._container.Resolve(controllerType);

            //release only after the request is processed
            request.RegisterForDispose(
                new ReleaseObject(
                    () => this._container.Release(controller)));

            return controller;

        }

        /// <summary>
        /// This class releases the object.
        /// It implements IDisposable making it a first class disposable object 
        /// </summary>
        private class ReleaseObject : IDisposable
        {
            private readonly Action _release;

            public ReleaseObject(Action release)
            {
                this._release = release;
            }

            public void Dispose()
            {
                this._release();
            }
        }

    }
}