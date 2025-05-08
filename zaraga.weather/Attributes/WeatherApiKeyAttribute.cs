using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zaraga.weather.Attributes;

[System.AttributeUsage(System.AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
sealed class WeatherApiKeyAttribute : System.Attribute
{
    public string WeatherKey { get; }
    public WeatherApiKeyAttribute(string WeatherApiKey)
    {
        this.WeatherKey = WeatherApiKey;
    }
}
