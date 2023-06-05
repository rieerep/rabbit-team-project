using Microsoft.AspNetCore.Mvc.Testing;

namespace web_api_test
{
	public class web_apiTest
	{
		[Fact]
		public async Task TestEndpoint_WhenUsing_Something()
		{

			await using var application = new WebApplicationFactory<Program>();
			using var client = application.CreateClient();

			var response = await client.GetStringAsync("/health-test");
			var temp = await client.GetStringAsync("/temperature");


			// Assert
			Assert.Equal("Hello World!", response);
		}
	}
}