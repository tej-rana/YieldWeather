using System;
using Microsoft.CSharp.RuntimeBinder;
using NUnit.Framework;
using System.Net;
using YieldWeather.Services.Test.Helper;
using System.IO;
using FluentAssertions;
using Newtonsoft.Json;
using YieldWeather.Domain;


namespace YieldWeather.Services.Test
{
    /// <summary>
    /// This fixture tests all weather services.
    /// This could easily be refactored into multiple classes
    /// when the classes grow in number and complexity.
    /// </summary>
    [TestFixture]
    public class WeatherServiceTests
    {   
        private ApplicationSettings.ForecastType _current_weather_url;
        private ApplicationSettings.ForecastType _five_day_forecast_url;

        private string _sydney_id_param = string.Empty;
        private string _hobart_id_param = string.Empty;

        private ApplicationSettings.WeatherUnits _celsius_units_param;
        private ApplicationSettings.WeatherUnits _farenheit_units_param;
        private ApplicationSettings.WeatherUnits _kelvin_units_param;


        /// <summary>
        /// Set all the urls up before running the test
        /// </summary>
        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            _current_weather_url = ApplicationSettings.ForecastType.CurrentWeather;
            _five_day_forecast_url = ApplicationSettings.ForecastType.FiveDayForecast;            

            _sydney_id_param = ApplicationSettings.SydneyCityId;
            _hobart_id_param = ApplicationSettings.HobartCityId;

            _celsius_units_param  = ApplicationSettings.WeatherUnits.Celsius;
            _farenheit_units_param = ApplicationSettings.WeatherUnits.Farenheit;
            _kelvin_units_param = ApplicationSettings.WeatherUnits.Kelvin;

        }



        [Test]
        public void Get_CurrentWeahter_For_Sydney_With_Celsius_Units_Should_Return_Values()
        {
         

            var webRequest = (HttpWebRequest)WebRequest.Create(WeatherHelper.GenerateUrl(_current_weather_url, _sydney_id_param, _celsius_units_param));
            webRequest.ContentType = "application/json;charset=\"utf-8\"";
            webRequest.Method = "GET";

            //this is the test object.
            //let's initialize it
            var responseText = string.Empty;

            //at this stage let's verify that it is empty
            responseText.Should().BeEmpty();

            //make the request and get json string back
            //TODO: Error handling if nothing gets back
            using (WebResponse response = webRequest.GetResponse())
            {
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = streamReader.ReadToEnd();
                }
            }

            

            //this is a basic fluent assertion test.
            //TODO: Need to expand into getting the JSON output and if the city reads "Sydney" or if there is min_temp greater than 0.
            responseText.Should().NotBeEmpty();
            
            
            //this needs to be dynamic to resolve at runtime
            //other alternate is to create the exact Class that matches all the parameters.
            dynamic obj = JsonConvert.DeserializeObject(responseText);

            var currentWeather = new CurrentWeatherContract();

            var main = obj.main;

            currentWeather.AirPressure = (main.pressure != null) ? (double)(main.pressure) : 0;

            currentWeather.MinTemp = (main.temp_min != null) ? (double)(main.temp_min) : 0;

            currentWeather.MaxTemp = (main.temp_max != null) ? (double)(main.temp_max) : 0;

            currentWeather.Humidity = (main.humidity != null) ? (int)(main.humidity) : 0;

            var rain = obj.rain;

            //unfortunately we need to access this with the named index property
            currentWeather.Rainfall = (rain != null) ? (double)(rain["3h"]) : 0;

            //Make assertions
            currentWeather.AirPressure.Should().BeGreaterThan(0);
            
            //I'm not sure if I can make other assertions like they're not 0. 
        }

        [Test]
        public void Get_CurrentWeahter_For_Hobart_With_Kelvin_Units_Should_Return_Values()
        {


            var webRequest = (HttpWebRequest)WebRequest.Create(WeatherHelper.GenerateUrl(_current_weather_url, _sydney_id_param, _kelvin_units_param));
            webRequest.ContentType = "application/json;charset=\"utf-8\"";
            webRequest.Method = "GET";

            //this is the test object.
            //let's initialize it
            var responseText = string.Empty;

            //at this stage let's verify that it is empty
            responseText.Should().BeEmpty();

            //make the request and get json string back
            //TODO: Error handling if nothing gets back
            using (WebResponse response = webRequest.GetResponse())
            {
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = streamReader.ReadToEnd();
                }
            }



            //this is a basic fluent assertion test.
            //TODO: Need to expand into getting the JSON output and if the city reads "Sydney" or if there is min_temp greater than 0.
            responseText.Should().NotBeEmpty();


            //this needs to be dynamic to resolve at runtime
            //other alternate is to create the exact Class that matches all the parameters.
            dynamic obj = JsonConvert.DeserializeObject(responseText);

            var currentWeather = new CurrentWeatherContract();

            var main = obj.main;

            currentWeather.AirPressure = (main.pressure != null) ? (double)(main.pressure) : 0;

            currentWeather.MinTemp = (main.temp_min != null) ? (double)(main.temp_min) : 0;

            currentWeather.MaxTemp = (main.temp_max != null) ? (double)(main.temp_max) : 0;

            currentWeather.Humidity = (main.humidity != null) ? (int)(main.humidity) : 0;

            var rain = obj.rain;

            //unfortunately we need to access this with the named index property
            currentWeather.Rainfall = (rain != null) ? (double)(rain["3h"]) : 0;

            //Make assertions
            currentWeather.AirPressure.Should().BeGreaterThan(0);

            //pretty sure Kelvin temperature cannot be 0 or less 
            //For current temperature, the min and max is same.
            currentWeather.MinTemp.Should().BeGreaterThan(0).And.ShouldBeEquivalentTo(currentWeather.MaxTemp);


        }

        [Ignore("Need to add date compare logic")]
        public void Get_FiveDay_Weahter_For_Sydney_With_Celsius_Units_Should_Return_Values()
        {


            var webRequest = (HttpWebRequest)WebRequest.Create(WeatherHelper.GenerateUrl(_five_day_forecast_url, _sydney_id_param, _celsius_units_param));
            webRequest.ContentType = "application/json;charset=\"utf-8\"";
            webRequest.Method = "GET";

            //this is the test object.
            //let's initialize it
            var responseText = string.Empty;

            //at this stage let's verify that it is empty
            responseText.Should().BeEmpty();

            //make the request and get json string back
            //TODO: Error handling if nothing gets back
            using (WebResponse response = webRequest.GetResponse())
            {
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = streamReader.ReadToEnd();
                }
            }



            //this is a basic fluent assertion test.
            //TODO: Need to expand into getting the JSON output and if the city reads "Sydney" or if there is min_temp greater than 0.
            responseText.Should().NotBeEmpty();


            //this needs to be dynamic to resolve at runtime
            //other alternate is to create the exact Class that matches all the parameters.
            dynamic obj = JsonConvert.DeserializeObject(responseText);

            var fiveDayForecast = new FiveDayWeatherForecastContract();

            int countOfDays = -1;
            //Loop through each list
            foreach (var curObj in obj.list)
            {
                //a check to make sure we only get five days of data even if it returns more
                while (countOfDays < 6) {
                    
                    //TODO:do a date compare to check if it is the same date...
                    //create new weather object only if it is a different date..
                    //and then add the countOfDays
                    fiveDayForecast[countOfDays] = new CurrentWeatherContract();                    
                    countOfDays++;



                    var main = curObj.main;

                    //TODO: if same date compare the max value for all except do the opposite for min and update the object
                    fiveDayForecast[countOfDays].AirPressure = (main.pressure != null) ? (double)(main.pressure) : 0;

                    fiveDayForecast[countOfDays].MinTemp = (main.temp_min != null) ? (double)(main.temp_min) : 0;

                    fiveDayForecast[countOfDays].MaxTemp = (main.temp_max != null) ? (double)(main.temp_max) : 0;

                    fiveDayForecast[countOfDays].Humidity = (main.humidity != null) ? (int)(main.humidity) : 0;

                    var rain = obj.rain;

                    //unfortunately we need to access this with the named index property
                    fiveDayForecast[countOfDays].Rainfall = (rain != null) ? (double)(rain["3h"]) : 0;

                    
                  }
            }

            //TODO: Add Assestions
            
                        
        }
    }
}
