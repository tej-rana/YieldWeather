using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldWeather.Domain;

namespace YieldWeather.Services
{
    /// <summary>
    /// The generic service interface that will be injected into controllers
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Gets an IContract (domain) for IServiceContract (service)
        /// </summary>
        /// <param name="IServiceContract">Service contract</param>
        /// <returns>IContract</returns>
        IContract Get(IServiceContract serviceContract);
    }
}
