using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YieldWeather.Services.Test.Helper
{
    /// <summary>
    /// Helper class for Weather Classes
    /// </summary>
    public static class WeatherHelper
    {
        /// <summary>
        /// Generates Url from weather parameters
        /// </summary>
        /// <param name="forecastType">ApplicationSettings.ForecastType</param>
        /// <param name="id_param">City Id</param>        
        /// <param name="units">ApplicationSettings.WeatherUnits</param>
        /// <returns></returns>
        public static string GenerateUrl(ApplicationSettings.ForecastType forecastType, string id_param, ApplicationSettings.WeatherUnits units)
        {
            //build url and parameters for web request
            string forecast_url = string.Empty;            
            string units_param = string.Empty;
            string token = ApplicationSettings.Token;

            //TODO: throw exception if any of these is empty.
            switch (forecastType)
            {
                case ApplicationSettings.ForecastType.CurrentWeather:
                    forecast_url = ApplicationSettings.CurrentWeather;
                    break;
                case ApplicationSettings.ForecastType.FiveDayForecast:
                    forecast_url = ApplicationSettings.FiveDayForecast;
                    break;
                default:
                    break; //TODO: can't get here Need to throw exception
            }

            switch (units)
            {
                case ApplicationSettings.WeatherUnits.Celsius:
                    units_param = ApplicationSettings.UnitsCelsuius;
                    break;
                case ApplicationSettings.WeatherUnits.Farenheit:
                    forecast_url = ApplicationSettings.UnitsFarenheit;
                    break;
                default:
                    break; //this is fine because default is Kelvin and takes no parameter
            }

            if (id_param == ApplicationSettings.SydneyCityId)
            {
                id_param = ApplicationSettings.SydneyCityId;
            }
            else
            {
                id_param = ApplicationSettings.HobartCityId;
            }


            return forecast_url + id_param + token + units_param;
        }
    }
}
