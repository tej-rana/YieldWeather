using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YieldWeather.Startup))]
namespace YieldWeather
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
