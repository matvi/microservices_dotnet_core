using System.Collections.Generic;
using System.Net.Http;
using Action.Services.Activities;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace Actio.Services.Activities.Tests.Integrations
{
    public class WeatherForecastControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public WeatherForecastControllerTest()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async void WeatherForecastControllerGet_ShouldReturnStringContent()
        {
        //arrange
            var response = await _client.GetAsync("/WeatherForecast");
            response.EnsureSuccessStatusCode();

        //act
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<WeatherForecast>>(content);
        //assert
            result.Should().HaveCount(5);
        }
    }
}