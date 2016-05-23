using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldWeather.Services
{
   public interface IServiceFactory
    {
        /// <summary>
        /// Get instance of CurrentWeatherService
        /// </summary>
        /// <returns>New instance of CurrentWeatherService</returns>
        IService GetCurrentWeatherService();


        /// <summary>
        /// Get instance of FiveDayWeatherService
        /// </summary>
        /// <returns>New instance of FiveDayWeatherService</returns>
        IService GetFiveDayWeatherService();

        /// <summary>
        /// Dispose the service
        /// </summary>
        /// <param name="service">IService instance</param>
        void Release(IService service);
    }
}
