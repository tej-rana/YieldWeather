using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldWeather.Domain;

namespace YieldWeather.Services
{
    public class CurrentWeatherService : IService
    {
        public IEnumerable<IContract> Get(params object[] objects)
        {
            throw new NotImplementedException();
        }
    }
}
