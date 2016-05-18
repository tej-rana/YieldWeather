using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldWeather.Domain
{
    /// <summary>
    /// Current weather contract interface
    /// </summary>
    public interface ICurrentWeatherContract: IContract
    {
        /// <summary>
        /// Minimum Temperature
        /// </summary>
         Decimal MinTemp { get; set; }
        /// <summary>
        /// Maximum Temperature
        /// </summary>
         Decimal MaxTemp { get; set; }

        /// <summary>
        /// Air Pressure
        /// </summary>
         Decimal AirPressure { get; set; }

        /// <summary>
        /// Humidity
        /// </summary>
         Int32 Humidity { get; set; }


        /// <summary>
        /// Rainfall
        /// </summary>
         Int32 Rainfall { get; set; }
        
    }
}
