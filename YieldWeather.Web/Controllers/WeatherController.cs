using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YieldWeather.Services;
using YieldWeather.Web.Helpers;


namespace YieldWeather.Web.Controllers
{
    public class WeatherController : Controller
    {
        // GET: Weather View
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetFiveDayForecast(string city, string unit)
        {

            Services.Helper.ApplicationSettings.WeatherUnits units;

            if (unit.Equals(Services.Helper.ApplicationSettings.UnitsCelsuius)) {
                units = Services.Helper.ApplicationSettings.WeatherUnits.Celsius;
            }else if (unit.Equals(Services.Helper.ApplicationSettings.UnitsFarenheit))
            {
                units = Services.Helper.ApplicationSettings.WeatherUnits.Farenheit;
            }else
            {
                units = Services.Helper.ApplicationSettings.WeatherUnits.Kelvin;
            }

            IServiceContract serviceContract = new ServiceContract
            {
                CityId = city,
                Units = units,
                ForecastType = Services.Helper.ApplicationSettings.ForecastType.FiveDayForecast

            };

            //TODO: Resolve Service type to call 


            //Return response

            return View("Index");
        }

        [HttpPost]
        public ActionResult GetCurrentWeather(string city, string unit)
        {
            Services.Helper.ApplicationSettings.WeatherUnits units;

            if (unit.Equals(Services.Helper.ApplicationSettings.UnitsCelsuius))
            {
                units = Services.Helper.ApplicationSettings.WeatherUnits.Celsius;
            }
            else if (unit.Equals(Services.Helper.ApplicationSettings.UnitsFarenheit))
            {
                units = Services.Helper.ApplicationSettings.WeatherUnits.Farenheit;
            }
            else
            {
                units = Services.Helper.ApplicationSettings.WeatherUnits.Kelvin;
            }

            IServiceContract serviceContract = new ServiceContract
            {
                CityId = city,
                Units = units,
                ForecastType = Services.Helper.ApplicationSettings.ForecastType.CurrentWeather

            };

            //TODO: Resolve Service type to call 


            //Return response
            return View("Index");
        }
    }
}