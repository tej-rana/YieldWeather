using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using YieldWeather.Domain;
using YieldWeather.Services.Helper;
namespace YieldWeather.Services
{
    public class FiveDayWeatherService : IService
    {
        FiveDayWeatherForecastContract _contract;

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
            //all of this is copied from the test

            _contract = new FiveDayWeatherForecastContract();

            dynamic obj = JsonConvert.DeserializeObject(responseText);            

            var previousDate = new DateTime();

            int countOfDays = -1;
            //Loop through each list
            foreach (var curObj in obj.list)
            {

                //a. Get the new date from the list
                var dt_milliseconds = Convert.ToInt64(curObj.dt * 1000);

                var posixTime = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);

                DateTime newDate = posixTime.AddMilliseconds(dt_milliseconds);


                //b. if this is the first run or the data is for a new date then 
                //increase the count
                if (countOfDays == -1 || newDate.Date > previousDate.Date)
                {

                    countOfDays++;
                    _contract[countOfDays] = new CurrentWeatherContract();
                }

                previousDate = newDate;



                //c. Update the values for the index. The last one will overwrite all of it.
                //Assumption: All dates are ordered from old to new date in the response.                   

                var main = curObj.main;


                _contract[countOfDays].AirPressure = (main.pressure != null) ? (double)(main.pressure) : 0;

                _contract[countOfDays].MinTemp = (main.temp_min != null) ? (double)(main.temp_min) : 0;

                _contract[countOfDays].MaxTemp = (main.temp_max != null) ? (double)(main.temp_max) : 0;

                _contract[countOfDays].Humidity = (main.humidity != null) ? (int)(main.humidity) : 0;

                var rain = obj.rain;

                //unfortunately we need to access this with the named index property because they've poorly named it.
                _contract[countOfDays].Rainfall = (rain != null) ? (double)(rain["3h"]) : 0;



            }
        }

        private HttpWebRequest CreateWebRequest(string cityId, ApplicationSettings.ForecastType forecastType, ApplicationSettings.WeatherUnits units)
        {

            //added this method to remove guess work of what parameter goes first
            var requestUrl = WeatherHelper.GenerateUrl(forecastType, cityId, units);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
            webRequest.ContentType = "application/json;charset=\"utf-8\"";
            webRequest.Method = "GET";

            return webRequest;
        }
    }
}
