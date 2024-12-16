using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLitePCL;
using WeatherApp.Models;
using WeatherApp.Services;
namespace WeatherApp.Pages;

public class IndexModel : PageModel
{
    //private readonly ILogger<IndexModel> _logger;
    private readonly WeatherApp.Data.WeatherAppContext _context;
    private readonly IWeatherService _weatherhttpclient;
    //private readonly IHttpClientFactory _httpClientFactory;
    //ILogger<IndexModel> logger,
    public IndexModel(WeatherApp.Data.WeatherAppContext context, IWeatherService weatherhttpclient)
    {
        //_logger = logger;
        _context = context;
        _weatherhttpclient = weatherhttpclient;
       // _httpClientFactory = httpClientFactory;

    }
    //public IList<City> Cites { get; set; } = default!;

    string appid = "add your own api key";
    public City City {get; set;} = default!;
    public Weather Weather {get; set; }

    [BindProperty(SupportsGet = true)]
    [Required(ErrorMessage = "Title is required.")]
    public string Search_String {get; set; } = "Detroit";
    public bool status_code_ok { get; set; } = true;

    public async Task<IActionResult> OnGetAsync()
    {
        //default/on page start up //need to fix
        try{
            HttpResponseMessage response = await _weatherhttpclient.GetWeatherAsync(Search_String);
            response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response is unsuccessful
            TempData["Search_String"] = Search_String;
            TempData["status_code_ok"] = "ok";
                
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject? json_response_body = JsonConvert.DeserializeObject<JObject> (responseBody);

            

            if(City == default || City == null){

                if(!await _context.City.AnyAsync(c => c.Name == Search_String)){
                    //city is not in database.
                    await _context.City.AddAsync(
                        new City {
                            Name = Search_String, //(string)json_response_body[""]
                            Lon = (double)json_response_body["coord"]["lon"],
                            Lat = (double)json_response_body["coord"]["lat"]
                        }
                    );
                
                    await _context.SaveChangesAsync();
                }
                
            
            }
           
            //then add weather condition to db
            City = await _context.City.SingleAsync(c=> c.Name == Search_String);

            if(!await _context.Weather.AnyAsync(c => c.Dt == (int)json_response_body["dt"])){
                await _context.Weather.AddAsync(
                    new Weather {
                        Condition = (string)json_response_body["weather"][0]["main"],
                        Description = (string)json_response_body["weather"][0]["description"],
                        Temp =  (double)json_response_body["main"]["temp"],
                        Temp_min = (double)json_response_body["main"]["temp_min"],
                        Temp_max = (double)json_response_body["main"]["temp_max"],
                        Dt = (int)json_response_body["dt"],
                        Timezone = (int)json_response_body["timezone"],
                        Lon = City.Lon,
                        Lat = City.Lat
                    }
                );
                await _context.SaveChangesAsync();
            }

            Weather = await _context.Weather.SingleAsync
            (
                w=> w.Lon == City.Lon && w.Lat == City.Lat && w.Dt == (int)json_response_body["dt"] 
            );
            TempData["LastCity"] = City.Name;
            JObject WeatherObject = new JObject(
                new JProperty("CityName", City.Name),
                new JProperty("Lon", City.Lon),
                new JProperty("Lat", City.Lat),
                new JProperty("Description", Weather.Description),
                new JProperty("Condition", Weather.Condition),
                new JProperty("Temp-min", Weather.Temp_min),
                new JProperty("Temp-max", Weather.Temp_max),
                new JProperty("Dt", Weather.Dt),
                new JProperty("TimeZone", Weather.Timezone)
            );
            TempData["WeatherCondition"] = WeatherObject.ToString();
            // TempData["WeatherDescription"] = Weather.Description;
            // TempData["WeatherMin-Temp"] = Weather.Temp_min;
            // TempData["WeatherMax-Temp"] = Weather.Temp_max;
            // TempData["WeatherTime"] = Weather.Dt;
            // TempData["WeatherTimeZone"] = Weather.Timezone;
            //TempData["LastWeather"] = JsonConvert.SerializeObject(Weather);

           
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);

            //TempData["WeatherCondition"] =  TempData["WeatherCondition"];
            //TempData["Search_String"] = TempData["Search_String"];
            TempData.Keep("WeatherCondition"); 
            TempData.Keep("Search_String");
            TempData["status_code_ok"] = "not_ok";
            //return RedirectToPage("Index");
            return Page();
        }
        return Page();
    }
}
