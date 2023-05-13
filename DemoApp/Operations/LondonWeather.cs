using CSharpScriptOperations;
using Newtonsoft.Json;
using System.Net;

namespace DemoApp.Operations;

[OperationDescription("Print the current weather in London", priority: 4)]
class LondonWeather : IOperation
{
    public async Task RunAsync()
    {
        // Attempt to get the weather and print it to the console
        try
        {
            string apiUrl = "https://www.metaweather.com/api/location/44418/";
            var json = await (new HttpClient()).GetStringAsync(apiUrl);
            var weatherObj = JsonConvert.DeserializeObject<dynamic>(json);
            Console.WriteLine("The weather in London is currently: " + weatherObj.consolidated_weather[0].weather_state_name);
        }
        catch
        {
            Console.WriteLine("Failed to get weather in London.");
        }
    }
}
