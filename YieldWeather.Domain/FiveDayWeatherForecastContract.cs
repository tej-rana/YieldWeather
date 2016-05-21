using System.Collections;
using System.Collections.Generic;


namespace YieldWeather.Domain
{
    /// <summary>
    /// Five day weather forecast contract interface
    /// </summary>
    public class FiveDayWeatherForecastContract : IFiveDayWeatherForecastContract
    {        
        /// <summary>
        /// Returns list of contracts 
        /// </summary>
        public CurrentWeatherContract this [int index]
        {
            get
            {
                    return _fiveDayForecastList[index];
            }
            set
            {
                _fiveDayForecastList.Insert(index, value);
            }
       }

        //We only want five becayuse that is what the class name reads.
        //Although this is a magic variable I don't think this is a problem because of the name of the class itself.
        List<CurrentWeatherContract> _fiveDayForecastList = new List<CurrentWeatherContract>(5);


        public IEnumerator<ICurrentWeatherContract> GetEnumerator()
        {
            return _fiveDayForecastList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
