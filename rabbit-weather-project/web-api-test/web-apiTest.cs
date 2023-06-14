using Microsoft.AspNetCore.Mvc.Testing;
using System.Diagnostics;
using System.Net.Http;
using web_api;

namespace web_api_test
{
    internal record HealthStatus(string Status);
    internal record WeatherTemp(int Temp);

    // Skriv ett nytt typ av record enligt ovan med v�derdata
    // Skriv sedan ett test med r�tt typ av data
    // Gl�m inte att starta API:et med dotnet watch innan man k�r testern
    public class web_apiTest
    {

        public static string? CustomPort = Environment.GetEnvironmentVariable("CUSTOMPORT");

        private readonly HttpClient _httpClient = new()
        {

            BaseAddress = new Uri($"http://localhost:{(string.IsNullOrEmpty(CustomPort) ? "20400" : CustomPort)}")
        };

        [Fact]
        public async Task GivenARequest_WhenCallingHealthCheck_ThenTheAPIReturnsExpectedResponse()
        {
            // Arrange.
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var expectedContent = new HealthStatus("Connection is working with server.");
            var stopwatch = Stopwatch.StartNew();

            // Act.
            var response = await _httpClient.GetAsync("/healthcheck");

            // Assert.
            await TestHelpers.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
        }

        [Fact]
        public async Task Temperature_GivenARequest_WhenCallingTempCheck_ThenAPIReturnsExptectedResponse()
        {
            // Arrange 
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var expectedContent = new WeatherTemp(19);
            var stopwatch = Stopwatch.StartNew();


            // Act
            var response = await _httpClient.GetAsync("/weatherdata");

            // Assert
            await TestHelpers.AssertResponseWithContentAsync(stopwatch, response, expectedStatusCode, expectedContent);

        }

        [Fact]
        public async Task Temperature_GivenARequest_WhenCallingCurrentWeather_ThenAPIReturnsExptectedResponse()
        {
            // Arrange 
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var expectedContent = new
			{
				weather = new[] {
				new
				{
					name = "stockholm",
					weather = "sunny",
					wind = 15.0
				},
				new
				{
					name = "eskilstuna",
					weather = "cloudy",
					wind = 3.2
				}
				}
			};
			var stopwatch = Stopwatch.StartNew();


            // Act
            var response = await _httpClient.GetAsync("/currentweather");

            // Assert
            await TestHelpers.AssertResponseWithSerializedContentAsync(stopwatch, response, expectedStatusCode, expectedContent);

        }
    }
}