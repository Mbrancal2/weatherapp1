namespace WeatherApp.Services;
public class WeatherHttpClient : IWeatherService
{
    private readonly HttpClient _httpClient;

    private readonly string APIKey  = "add your own api key";
    public WeatherHttpClient(HttpClient httpClient){
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
        //"https://api.openweathermap.org/data/2.5/weather?appid=b686b53925c6c24c83629feb20580318");

    }
    public async Task<HttpResponseMessage> GetWeatherAsync(string city)
    {
        //weather?q={Search_String}&appid={appid}
        //HttpResponseMessage t =  await _httpClient.GetAsync($"weather?q={city}&appid={APIKey}");
        //Console.WriteLine(t);
        return await _httpClient.GetAsync($"weather?q={city}&appid={APIKey}");//$"&q={city}");
    }
}

public interface IWeatherService
{
    Task<HttpResponseMessage> GetWeatherAsync(string city);
}

