using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldWeather.Services.Helper;
using static YieldWeather.Services.Helper.ApplicationSettings;

namespace YieldWeather.Services
{
    /// <summary>
    /// Service Contract
    /// </summary>
    public class ServiceContract : IServiceContract
    {        

        /// <summary>
        /// City Id
        /// </summary>
        public string CityId { get; set; }

        /// <summary>
        /// The type of Forecast
        /// </summary>
        public ForecastType ForecastType { get; set; }

        /// <summary>
        /// The units of measurement for temperature
        /// </summary>
        public WeatherUnits Unit { get; set; }
    }
}
