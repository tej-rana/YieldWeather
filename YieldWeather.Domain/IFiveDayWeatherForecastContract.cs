using System.Collections.Generic;


namespace YieldWeather.Domain
{
    /// <summary>
    /// Five day weather forecast contract interface.
    /// Because we have the same contract for this as ICurrentWeatherContract but only
    /// aggregates it, we can extend the collection of ICurrentWeatherContract
    /// </summary>
    public interface IFiveDayWeatherForecastContract : IContract, IEnumerable<ICurrentWeatherContract>
    {   
    }
}
