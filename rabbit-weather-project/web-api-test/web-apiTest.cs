using Microsoft.AspNetCore.Mvc.Testing;
using System.Diagnostics;
using System.Net.Http;

namespace web_api_test
{
	// Test

	internal record HealthStatus(string Status);
	internal record WeatherTemp (int Temp);
	// Skriv ett nytt typ av record enligt ovan med väderdata
	// Skriv sedan ett test med rätt typ av data
	// Glöm inte att starta API:et med dotnet watch innan man kör testerna
	public class web_apiTest
	{
		private readonly HttpClient _httpClient = new()
		{
			BaseAddress = new Uri("http://localhost:20400")
		};

		[Fact]
		public async Task GivenARequest_WhenCallingHealthCheck_ThenTheAPIReturnsExpectedResponse()
		{
			// Arrange.
			var expectedStatusCode = System.Net.HttpStatusCode.OK;
			var expectedContent = new HealthStatus ("Connection is working with server.");
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
	}
}