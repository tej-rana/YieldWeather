using System;


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
        public double MinTemp { get; set; }
        /// <summary>
        /// Maximum Temperature
        /// </summary>
        public double MaxTemp { get; set; }

        /// <summary>
        /// Air Pressure
        /// </summary>
        public double AirPressure { get; set; }

        /// <summary>
        /// Humidity
        /// </summary>
        public int Humidity { get; set; }


        /// <summary>
        /// Rainfall
        /// </summary>
        public double Rainfall { get; set; }
    }
}
