using CSharpScriptOperations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Operations
{
    class LondonWeather : IOperation
    {
        public string Description => 
            "Print the current weather in London";

        public async Task RunAsync()
        {
            // Attempts to get the 
            try
            {
                string apiUrl = "https://www.metaweather.com/api/location/44418/";
                var json = (new WebClient()).DownloadString(apiUrl);
                var weatherObj = JsonConvert.DeserializeObject<dynamic>(json);
                Console.WriteLine("The weather in London is currently: " + weatherObj.consolidated_weather[0].weather_state_name);
            }
            catch
            {
                Console.WriteLine("Failed to get weather in London.");
            }
        }
    }
}
