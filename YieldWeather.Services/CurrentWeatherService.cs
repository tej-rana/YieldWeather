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
