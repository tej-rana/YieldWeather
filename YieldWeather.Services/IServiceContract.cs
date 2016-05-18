using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YieldWeather.Services.Helper.ApplicationSettings;

namespace YieldWeather.Services
{
    /// <summary>
    /// This is a generic service interface that should be implemented by all models that 
    /// is used to map request parameters from web tier.    
    /// </summary>
    public interface IServiceContract
    {
        /// <summary>
        /// City Id
        /// </summary>
        string CityId { get; set; }

        /// <summary>
        /// The type of Forecast
        /// </summary>
        ForecastType ForecastType { get; set; }

        /// <summary>
        /// The units of measurement for temperature
        /// </summary>
        WeatherUnits Units { get; set; }
    }
}
