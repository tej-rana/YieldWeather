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
        /// Gets a collection of IContracts (domain) for IServiceContract (service)
        /// </summary>
        /// <param name="IServiceContract">Service contract</param>
        /// <returns>Collection of IContract</returns>
        IEnumerable<IContract> Get(IServiceContract serviceContract);
    }
}
