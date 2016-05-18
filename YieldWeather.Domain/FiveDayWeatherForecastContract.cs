using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        List<CurrentWeatherContract> _fiveDayForecastList = new List<CurrentWeatherContract>();
       

        IEnumerator<CurrentWeatherContract> GetEnumerator()
        {
            return _fiveDayForecastList.GetEnumerator();
        }

        IEnumerator<ICurrentWeatherContract> IEnumerable<ICurrentWeatherContract>.GetEnumerator()
        {
            return _fiveDayForecastList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
