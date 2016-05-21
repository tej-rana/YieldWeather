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

            _celsius_units_param = ApplicationSettings.WeatherUnits.Celsius;
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
            currentWeather.MinTemp.Should().BeGreaterThan(0);


        }

        //To test if the date returned is milliseconds or ticks
        [Test()]
        public void Weather_Response_Date_Test()
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

            //this needs to be dynamic to resolve at runtime
            //other alternate is to create the exact Class that matches all the parameters.
            dynamic obj = JsonConvert.DeserializeObject(responseText);

            var dt_milliseconds = Convert.ToInt64(obj.dt * 1000);

            var posixTime = DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc);

            DateTime date = posixTime.AddMilliseconds(dt_milliseconds);

            //Test. This is current weather so date should be today's date
            date.Date.ShouldBeEquivalentTo(DateTime.Today);

        }

        [Test]
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

            var fiveDayForecasts = new FiveDayWeatherForecastContract();

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
                        fiveDayForecasts[countOfDays] = new CurrentWeatherContract();
                    }

                    previousDate = newDate;



                    //c. Update the values for the index. The last one will overwrite all of it.
                    //Assumption: All dates are ordered from old to new date in the response.                   

                    var main = curObj.main;

                    
                    fiveDayForecasts[countOfDays].AirPressure = (main.pressure != null) ? (double)(main.pressure) : 0;

                    fiveDayForecasts[countOfDays].MinTemp = (main.temp_min != null) ? (double)(main.temp_min) : 0;

                    fiveDayForecasts[countOfDays].MaxTemp = (main.temp_max != null) ? (double)(main.temp_max) : 0;

                    fiveDayForecasts[countOfDays].Humidity = (main.humidity != null) ? (int)(main.humidity) : 0;

                    var rain = obj.rain;

                    //unfortunately we need to access this with the named index property because they've poorly named it.
                    fiveDayForecasts[countOfDays].Rainfall = (rain != null) ? (double)(rain["3h"]) : 0;

                

            }

            //Assertions
            //Probably not the best tests.
            fiveDayForecasts[0].AirPressure.Should().BeGreaterThan(0);
            fiveDayForecasts[1].AirPressure.Should().BeGreaterThan(0);
            fiveDayForecasts[2].AirPressure.Should().BeGreaterThan(0);
            fiveDayForecasts[3].AirPressure.Should().BeGreaterThan(0);
            fiveDayForecasts[4].AirPressure.Should().BeGreaterThan(0);


        }
    }
}
