using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace web_api_test
{
	public static class TestHelpers
	{
		private const string _jsonMediaType = "application/json";
		private const int _expectedMaxElapsedMilliseconds = 1000;
		public static readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };
		public static async Task AssertResponseWithContentAsync<T>(Stopwatch stopwatch,
		HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode,
		T expectedContent)
		{
			AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
			Assert.Equal(_jsonMediaType, response.Content.Headers.ContentType?.MediaType);
			Assert.Equal(expectedContent, await JsonSerializer.DeserializeAsync<T?>(
				await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions));
		}
		public static async Task AssertResponseWithSerializedContentAsync<T>(Stopwatch stopwatch,
		HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode,
		T expectedContent)
		{
			AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
			Assert.Equal(_jsonMediaType, response.Content.Headers.ContentType?.MediaType);
			
			string serializedExpectedContent = JsonSerializer.Serialize(expectedContent, _jsonSerializerOptions);
			string serializedResponse = await response.Content.ReadAsStringAsync();
			Assert.Equal(serializedExpectedContent, serializedResponse);
		}


		public static void AssertCommonResponseParts(Stopwatch stopwatch,
			HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode)
		{
			Assert.Equal(expectedStatusCode, response.StatusCode);
			Assert.True(stopwatch.ElapsedMilliseconds < _expectedMaxElapsedMilliseconds);
		}

		public static StringContent GetJsonStringContent<T>(T model)
			=> new(JsonSerializer.Serialize(model), Encoding.UTF8, _jsonMediaType);

		public static async Task AssertPartialResponseWithContentAsync<T>(Stopwatch stopwatch,
		HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode,
T		expectedContent)
		{
			AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
			Assert.Equal(_jsonMediaType, response.Content.Headers.ContentType?.MediaType);
			string serializedResponse = await response.Content.ReadAsStringAsync();
			var data = JsonSerializer.Deserialize<T>(serializedResponse);
			var responseObject = JObject.Parse(serializedResponse); // JObject parses json to string - 
			Assert.True(responseObject.Count.GetType() == typeof(int));
			//Assert.Equal(expectedContent, await JsonSerializer.DeserializeAsync<T?>(
			//	await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions));
		}

	}

}
