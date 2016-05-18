using System.Configuration;

namespace YieldWeather.Services.Helper
{
    /// <summary>
    /// This class gets application wide configuration
    /// </summary>
    public static class ApplicationSettings
    {

        #region OpenWeatherMap
        public static readonly string Token = ConfigurationManager.AppSettings["token"];
        public static readonly string SydneyCityId = ConfigurationManager.AppSettings["sydney_city_id"];
        public static readonly string HobartCityId = ConfigurationManager.AppSettings["hobart_city_id"];
        public static readonly string UnitsCelsuius = ConfigurationManager.AppSettings["units_celsius"];
        public static readonly string UnitsFarenheit = ConfigurationManager.AppSettings["units_farenheit"];
        public static readonly string CurrentWeather = ConfigurationManager.AppSettings["current_weather"];
        public static readonly string FiveDayForecast = ConfigurationManager.AppSettings["five_day_forecast"];
        
        /// <summary>
        /// Forecast Type
        /// </summary>
        public enum ForecastType
        {
            CurrentWeather,
            FiveDayForecast
        }
        /// <summary>
        /// Weather Units
        /// </summary>
        public enum WeatherUnits
        {
            Celsius,
            Farenheit,
            Kelvin
        }

        #endregion


    }
}
