using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

		//[Fact]
		//public async Task APICallCounter_GivenARequest_WhenCallingCallCounter_ThenAPIReturnsExptectedResponse()
		//{
		//	// Arrange 
		//	var expectedStatusCode = System.Net.HttpStatusCode.OK;
        //          var expectedContent = new
        //          {
        //              count = 5
        //          };
		//	var stopwatch = Stopwatch.StartNew();


		//	// Act
		//	var response = await _httpClient.GetAsync("/callcounter");

		//	// Assert
		//	await TestHelpers.AssertResponseWithSerializedContentAsync(stopwatch, response, expectedStatusCode, expectedContent);

		//}

        [Fact]
        public async Task APICallCounter_TestIfIncreses()
        {
            // Arrange 
            //var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var expectedMinCalls = 1;


            // Act
            var response = await _httpClient.GetAsync("/callcounter");
            var content = await response.Content.ReadAsStringAsync(); //takes api JSON repsonse result and assigns it to var content
            var responseObject = JObject.Parse(content); //JObject is a way to take JSON data and parse it so it can be accessed and used/manipulated in c#
            var actualCalls = (int)responseObject["calls"];//the int in the beginning means that we are typecasting string into int and then accessing that int with the
                                                           //name calls and the value is the number we are looking for to compare against our expected lowest numbner


            // Assert
            Assert.True(response.IsSuccessStatusCode);//if status code is between 200 and 299 it will return ture and assert is passed
            Assert.True(actualCalls >= expectedMinCalls);//if calls number is equal to or greater than 1 (so not 0) it will pass

        }

        [Theory]
        [InlineData("stockholm")]
        public async Task AddsFavoriteCity_WhenRequestedByClient(string favCity)
        {
            // Arrange 
            var stopwatch = Stopwatch.StartNew();
            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            
            var expectedContent = new { message = $"You added: {favCity}" };


            // Act
            var response = await _httpClient.GetAsync($"/add/city/{favCity}");


            // Assert
            //Assert.Equal(expectedContent, actual);
            await TestHelpers.AssertResponseWithSerializedContentAsync(stopwatch, response, expectedStatusCode, expectedContent);
        }
    }
}