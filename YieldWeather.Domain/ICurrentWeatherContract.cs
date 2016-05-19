using System;

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
         double MinTemp { get; set; }
        /// <summary>
        /// Maximum Temperature
        /// </summary>
        double MaxTemp { get; set; }

        /// <summary>
        /// Air Pressure
        /// </summary>
        double AirPressure { get; set; }

        /// <summary>
        /// Humidity
        /// </summary>
         int Humidity { get; set; }


        /// <summary>
        /// Rainfall
        /// </summary>
        double Rainfall { get; set; }
        
    }
}
