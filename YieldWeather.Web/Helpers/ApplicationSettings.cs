using System.Configuration;

namespace YieldWeather.Web.Helpers
{
    /// <summary>
    /// This class gets application wide configuration
    /// </summary>
    public static class ApplicationSettings
    {

        #region OpenWeatherMap
        //urls
        public static readonly string CurrentWeather = ConfigurationManager.AppSettings["current_weather"];
        public static readonly string FiveDayForecast = ConfigurationManager.AppSettings["five_day_forecast"];
        
        //city ids
        public static readonly string SydneyCityId = "?id=" + ConfigurationManager.AppSettings["sydney_city_id"];
        public static readonly string HobartCityId = "?id=" + ConfigurationManager.AppSettings["hobart_city_id"];
        
        //token
        public static readonly string Token = "&APPID=" + ConfigurationManager.AppSettings["token"];

        //units
        public static readonly string UnitsCelsuius = "&units=" + ConfigurationManager.AppSettings["units_celsius"];
        public static readonly string UnitsFarenheit = "&units=" + ConfigurationManager.AppSettings["units_farenheit"];
        public static readonly string UnitsKelvin = "&units=" + ConfigurationManager.AppSettings["units_kelvin"];


        #endregion


    }
}
