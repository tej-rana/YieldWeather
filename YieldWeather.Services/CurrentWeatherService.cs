using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using YieldWeather.Domain;
using YieldWeather.Services.Helper;
using static YieldWeather.Services.Helper.ApplicationSettings;

namespace YieldWeather.Services
{
    public class CurrentWeatherService : IService
    {        

        public IEnumerable<IContract> Get(IServiceContract contract)
        {
            var responseText = string.Empty;
            //construct the request object
            var httpWebRequest = CreateWebRequest(contract.CityId, contract.ForecastType, contract.Units);

            //make the request and get json string back
            //TODO: Error handling if nothing gets back
            using (WebResponse response = httpWebRequest.GetResponse())
            {
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = streamReader.ReadToEnd();
                }
            }




            return null;
        }

        private HttpWebRequest CreateWebRequest(string cityId, ForecastType forecastType, WeatherUnits units)
        {
            //TODO: clean up the magic string with Java style code-value lookup


            //build url and parameters for web request
            string forecast_url = string.Empty;
            string id_param = string.Empty;
            string units_param = string.Empty;

            switch (forecastType)
            {
                case ForecastType.CurrentWeather:
                    forecast_url = CurrentWeather;
                    break;
                case ForecastType.FiveDayForecast:
                    forecast_url = FiveDayForecast;
                    break;
                default:
                    break; //TODO: can't get here Need to throw exception
            }            

            switch (units)
            {
                case WeatherUnits.Celsius:
                    units_param = "&units=" + UnitsCelsuius;
                    break;
                case WeatherUnits.Farenheit:
                    forecast_url = "&units=" + UnitsFarenheit;
                    break;
                default:
                    break; //this is fine because default is Kelvin and takes no parameter
            }

            if (id_param == SydneyCityId)
            {
                id_param = "?id=" + SydneyCityId;
            }
            else
            {
                id_param = "?id=" + HobartCityId;
            }

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(forecast_url + id_param + units_param);
            webRequest.ContentType = "application/json;charset=\"utf-8\"";            
            webRequest.Method = "GET";

            return webRequest;
        }
    }
}
