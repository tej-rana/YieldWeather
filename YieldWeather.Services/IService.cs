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
        /// Gets a contracts depending on the parameters resolved during runtime.
        /// This allows controllers to call a single method passing multiple parameters.
        /// It depends on the implmenting class to resolve the parameters.
        /// I could easily have set string token but it only allows weather contract exchanges.
        /// </summary>
        /// <param name="objects">Array of dynamic objects.</param>
        /// <returns>Collection of IContract</returns>
        IEnumerable<IContract> Get(params object[] objects);
    }
}
