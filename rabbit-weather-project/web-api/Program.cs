namespace web_api
{

    public static class Global
    {
        public static int count = 0;

        public static void IncrementCounter()
        {
            count++;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            var app = builder.Build();
            app.UseCors();

            // GET Method to check health status
            app.MapGet("/healthcheck", () =>
            {
                Global.IncrementCounter();
                return new { Status = "Connection is working with server." };
            });

            // GET Method to check weather temp
            app.MapGet("/weatherdata", () =>
            {
                Global.IncrementCounter();
                return new { Temp = 19 };
            }).WithName("GetWeatherHardcoded");

            app.MapGet("/currentweather", () =>{ 
                Global.IncrementCounter();

                return new
                {
                    Weather = new[]{
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
            });

            // GET Method to check number of API calls
            app.MapGet("/callcounter", () => {
                Global.IncrementCounter();
                return new { calls = Global.count };
            });

            // GET Method to check number of API calls
            app.MapGet("/add/city/{favoriteCity}", (string favoriteCity) => {
                return new { city = "stockholm" };
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection();

            app.Run();
        }
    }
}


// The reason why you need this partial class definition,
// is that by default the Program.cs file is compiled into a private class Program,
// which can not be accessed by other projects.
public partial class Program { }
public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string? Summary { get; set; }
}
