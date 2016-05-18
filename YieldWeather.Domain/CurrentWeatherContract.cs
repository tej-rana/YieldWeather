using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldWeather.Domain
{
    /// <summary>
    /// Current weather contract class
    /// </summary>
    public class CurrentWeatherContract : ICurrentWeatherContract
    {
        /// <summary>
        /// Minimum Temperature
        /// </summary>
        public Decimal MinTemp { get; set; }
        /// <summary>
        /// Maximum Temperature
        /// </summary>
        public Decimal MaxTemp { get; set; }

        /// <summary>
        /// Air Pressure
        /// </summary>
        public Decimal AirPressure { get; set; }

        /// <summary>
        /// Humidity
        /// </summary>
        public Int32 Humidity { get; set; }


        /// <summary>
        /// Rainfall
        /// </summary>
        public Int32 Rainfall { get; set; }
    }
}
