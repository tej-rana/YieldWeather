using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YieldWeather.Domain;
using YieldWeather.Services;
using YieldWeather.Services.Helper;
using YieldWeather.Web.Helpers;
using YieldWeather.Web.Models;

namespace YieldWeather.Web.Controllers
{
    public class WeatherController : Controller
    {

        private readonly IServiceFactory _factory;


        /// <summary>
        /// Constructor that initializes IServiceFactory
        /// </summary>
        /// <param name="factory">IServiceFactory to resolve an IService implementation</param>

        public WeatherController(IServiceFactory factory)
        {
            _factory = factory;
        }


        /// <summary>
        /// Returns the default view
        /// </summary>
        /// <returns>Index View</returns>
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// Gets Five Day forecast 
        /// </summary>
        /// <param name="model">Weather Request Model</param>
        /// <returns>Action Result</returns>
        [HttpPost]
        public ActionResult GetFiveDayForecast(WeatherRequestModel model)
        {
            //TODO: Handling data model error
            if (model == null)
            {
                //TODO: error response. 

            }

            List<WeatherResponseModel> fiveDayForecastModel = new List<WeatherResponseModel>();

            Services.Helper.ApplicationSettings.WeatherUnits unit;

            //TODO: Fix this magic variable
            if (model.Unit.Equals(ConfigurationManager.AppSettings["units_celsius"]))
            {
                unit = Services.Helper.ApplicationSettings.WeatherUnits.Celsius;
            }
            else if (model.Unit.Equals(ConfigurationManager.AppSettings["units_farenheit"]))
            {
                unit = Services.Helper.ApplicationSettings.WeatherUnits.Farenheit;
            }
            else
            {
                unit = Services.Helper.ApplicationSettings.WeatherUnits.Kelvin;
            }

            IServiceContract serviceContract = new ServiceContract
            {
                CityId = model.CityId,
                Unit = unit,
                ForecastType = Services.Helper.ApplicationSettings.ForecastType.FiveDayForecast

            };

            try
            {
                var service = _factory.GetFiveDayWeatherService();

                var contract = service.Get(serviceContract);

                fiveDayForecastModel = (List<WeatherResponseModel>)MapIContract(contract, Services.Helper.ApplicationSettings.ForecastType.FiveDayForecast);

                _factory.Release(service);
            }
            catch (Exception ex)
            {
                //TODO: Logging and error message return
            }

            //TODO: Better way of extracting this

            if (model.CityId.Equals(ConfigurationManager.AppSettings["sydney_city_id"]))
            {
                ViewBag.City = "Sydney, NSW \n Australia";
            }
            else
            { ViewBag.City = "Hobart, TAZ \n Australia"; }

            //Return response
            return View("FiveDayWeather", fiveDayForecastModel);
        }



        /// <summary>
        /// Gets Current Weather forecast 
        /// </summary>
        /// <param name="model">Weather Request Model</param>
        /// <returns>Action Result</returns>
        [HttpPost]
        public ActionResult GetCurrentWeather(WeatherRequestModel model)
        {
            //TODO: Handling data model error
            if (model == null)
            {
                //TODO: error response. 

            }

            WeatherResponseModel currentWeatherForecastModel = new WeatherResponseModel();

            Services.Helper.ApplicationSettings.WeatherUnits unit;
            //TODO: Fix this magic variable
            if (model.Unit.Equals(ConfigurationManager.AppSettings["units_celsius"]))
            {
                unit = Services.Helper.ApplicationSettings.WeatherUnits.Celsius;
            }
            else if (model.Unit.Equals(ConfigurationManager.AppSettings["units_farenheit"]))
            {
                unit = Services.Helper.ApplicationSettings.WeatherUnits.Farenheit;
            }
            else
            {
                unit = Services.Helper.ApplicationSettings.WeatherUnits.Kelvin;
            }

            IServiceContract serviceContract = new ServiceContract
            {
                CityId = model.CityId,
                Unit = unit,
                ForecastType = Services.Helper.ApplicationSettings.ForecastType.CurrentWeather

            };

            try
            {
                var service = _factory.GetCurrentWeatherService();

                var contract = service.Get(serviceContract);

                currentWeatherForecastModel = (WeatherResponseModel)MapIContract(contract, Services.Helper.ApplicationSettings.ForecastType.CurrentWeather);

                _factory.Release(service);
            }
            catch (Exception ex)
            {
                //TODO: Logging and error message return
            }

            if (model.CityId.Equals(ConfigurationManager.AppSettings["sydney_city_id"]))
            {
                ViewBag.City = "Sydney, NSW \n Australia";
            }
            else
            { ViewBag.City = "Hobart, TAZ \n Australia"; }
            //Return response
            return View("CurrentWeather", currentWeatherForecastModel);
        }


        private object MapIContract(IContract contract, Services.Helper.ApplicationSettings.ForecastType forecastType)
        {

            if (forecastType.Equals(Services.Helper.ApplicationSettings.ForecastType.CurrentWeather))
            {
                var currentWeatherContract = (CurrentWeatherContract)contract;

                var currentWeatherForecastModel = new WeatherResponseModel
                {
                    AirPressure = currentWeatherContract.AirPressure,
                    Humidity = currentWeatherContract.Humidity,
                    MinTemp = currentWeatherContract.MinTemp,
                    MaxTemp = currentWeatherContract.MaxTemp,
                    Rainfall = currentWeatherContract.Rainfall
                };

                return currentWeatherForecastModel;
            }
            else
            {
                var fiveDayWeatherContract = (FiveDayWeatherForecastContract)contract;

                var fiveDayWeatherForecastModel = new List<WeatherResponseModel>();

                foreach (var forecast in fiveDayWeatherContract)
                {
                    var currentWeatherForecastModel = new WeatherResponseModel
                    {
                        AirPressure = forecast.AirPressure,
                        Humidity = forecast.Humidity,
                        MinTemp = forecast.MinTemp,
                        MaxTemp = forecast.MaxTemp,
                        Rainfall = forecast.Rainfall
                    };

                    fiveDayWeatherForecastModel.Add(currentWeatherForecastModel);
                }

                return fiveDayWeatherForecastModel;
            }
        }
    }
}