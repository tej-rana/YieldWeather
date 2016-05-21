using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using YieldWeather.Domain;
using YieldWeather.Services.Helper;

namespace YieldWeather.Services
{
    public class CurrentWeatherService : IService
    {

        CurrentWeatherContract _contract;

        public IContract Get(IServiceContract contract)
        {
            //construct the request object
            var httpWebRequest = CreateWebRequest(contract.CityId, contract.ForecastType, contract.Units);

            var responseText = string.Empty;

            //make the request and get json string back
            //TODO: Error handling if nothing gets back
            using (WebResponse response = httpWebRequest.GetResponse())
            {
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = streamReader.ReadToEnd();
                }
            }

            ExtractCurrentWeather(responseText);

            return _contract;
        }

        private void ExtractCurrentWeather(string responseText)
        {
            _contract = new CurrentWeatherContract();

            //all of this is copied from the test
            dynamic obj = JsonConvert.DeserializeObject(responseText);
            

            var main = obj.main;

            _contract.AirPressure = (main.pressure != null) ? (double)(main.pressure) : 0;

            _contract.MinTemp = (main.temp_min != null) ? (double)(main.temp_min) : 0;

            _contract.MaxTemp = (main.temp_max != null) ? (double)(main.temp_max) : 0;

            _contract.Humidity = (main.humidity != null) ? (int)(main.humidity) : 0;

            var rain = obj.rain;

            //unfortunately we need to access this with the named index property
            _contract.Rainfall = (rain != null) ? (double)(rain["3h"]) : 0;
        }

        private HttpWebRequest CreateWebRequest(string cityId, ApplicationSettings.ForecastType forecastType, ApplicationSettings.WeatherUnits units)
        {  

            //added this method to remove guess work of what parameter goes first
            var requestUrl = WeatherHelper.GenerateUrl(forecastType, cityId,  units);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
            webRequest.ContentType = "application/json;charset=\"utf-8\"";            
            webRequest.Method = "GET";

            return webRequest;
        }

        
    }
}
