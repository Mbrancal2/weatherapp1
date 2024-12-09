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

    string appid = "Add your own api key";
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
                
            string responseBody = await response.Content.ReadAsStringAsync();
            JObject? json_response_body = JsonConvert.DeserializeObject<JObject> (responseBody);

            

            if(City == default || City == null){
                // HttpResponseMessage response = await _weatherhttpclient.GetWeatherAsync(Search_String);
                // response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response is unsuccessful
                
                // string responseBody = await response.Content.ReadAsStringAsync();
                // JObject? json_response_body = JsonConvert.DeserializeObject<JObject> (responseBody);

                //City = await _context.City.SingleAsync(c => c.Name == Search_String );
                //Console.WriteLine(json_response_body);

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
                //City = await _context.City.SingleAsync(c=> c.Name == Search_String);

                // //then add weather condition to db
                // City = await _context.City.SingleAsync(c=> c.Name == Search_String);

                // if(!await _context.Weather.AnyAsync(c => c.Dt == (int)json_response_body["dt"])){
                //     await _context.Weather.AddAsync(
                //         new Weather {
                //             Condition = (string)json_response_body["weather"][0]["main"],
                //             Description = (string)json_response_body["weather"][0]["description"],
                //             Temp =  (double)json_response_body["main"]["temp"],
                //             Temp_min = (double)json_response_body["main"]["temp_min"],
                //             Temp_max = (double)json_response_body["main"]["temp_max"],
                //             Dt = (int)json_response_body["dt"],
                //             Timezone = (int)json_response_body["timezone"],
                //             Lon = City.Lon,
                //             Lat = City.Lat
                //         }
                //     );
                //     await _context.SaveChangesAsync();
                //     TempData["Search_String"] = Search_String;
                // }

                // Weather = await _context.Weather.SingleAsync
                // (
                //     w=> w.Lon == City.Lon && w.Lat == City.Lat && w.Dt == (int)json_response_body["dt"] 
                // );

            
                // City = new City
                // {
                //     Name = (string)json_response_body["name"],
                //     Lon = (double)json_response_body["coord"]["lon"],
                //     Lat = (double)json_response_body["coord"]["lat"]
                // };

                // Weather = new Weather {
                //     Condition = (string)json_response_body["weather"][0]["main"],
                //     Description = (string)json_response_body["weather"][0]["description"],
                //     Temp =  (double)json_response_body["main"]["temp"],
                //     Temp_min = (double)json_response_body["main"]["temp_min"],
                //     Temp_max = (double)json_response_body["main"]["temp_max"],
                //     Dt = (int)json_response_body["dt"],
                //     Timezone = (int)json_response_body["timezone"],
                //     Lon = City.Lon,
                //     Lat = City.Lat
                // };
                // TempData["city_name"] = City.Name;
            
            }
            //else
            //{
                // HttpResponseMessage response = await _weatherhttpclient.GetWeatherAsync(Search_String);
                // response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response is unsuccessful
                
                // string responseBody = await response.Content.ReadAsStringAsync();
                // JObject? json_response_body = JsonConvert.DeserializeObject<JObject> (responseBody);
                // if(!await _context.Weather.AnyAsync(c => c.Dt == (int)json_response_body["dt"])){
                //     await _context.Weather.AddAsync(
                //         new Weather {
                //             Condition = (string)json_response_body["weather"][0]["main"],
                //             Description = (string)json_response_body["weather"][0]["description"],
                //             Temp =  (double)json_response_body["main"]["temp"],
                //             Temp_min = (double)json_response_body["main"]["temp_min"],
                //             Temp_max = (double)json_response_body["main"]["temp_max"],
                //             Dt = (int)json_response_body["dt"],
                //             Timezone = (int)json_response_body["timezone"],
                //             Lon = City.Lon,
                //             Lat = City.Lat
                //         }
                //     );
                // }
                
                //City = await _context.City.SingleAsync(c=> c.Name == Search_String);

                //Weather = await _context.Weather.SingleAsync
               //(
                    //w=> w.Lon == City.Lon && w.Lat == City.Lat && w.Dt == (int)json_response_body["dt"] 
                //);

           //}
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
                    TempData["Search_String"] = Search_String;
                }

                Weather = await _context.Weather.SingleAsync
                (
                    w=> w.Lon == City.Lon && w.Lat == City.Lat && w.Dt == (int)json_response_body["dt"] 
                );


            // if (await _context.City.AnyAsync(c => c.Name == Search_String))
            // {

            //     City =  await _context.City.SingleAsync(c => c.Name == Search_String);
            //     TempData["city_name"] = City.Name;
            // }
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            TempData["status_code_ok"] = "not_ok";
            return RedirectToPage("Index");
        }
        return Page();
    }



    // public async Task<IActionResult> OnPostAsync(){
    //     try{
    //         HttpResponseMessage response = await _weatherhttpclient.GetWeatherAsync(Search_String);
    //         response.EnsureSuccessStatusCode();
    //         string responseBody = await response.Content.ReadAsStringAsync();
    //         JObject? json_response_body = JsonConvert.DeserializeObject<JObject> (responseBody);
            
    //         if(!await _context.City.AnyAsync(c => c.Name == Search_String)){
    //             //city is not in database.
    //             await _context.City.AddAsync(
    //                 new City {
    //                         Name = Search_String, //(string)json_response_body[""]
    //                         Lon = (double)json_response_body["coord"]["lon"],
    //                         Lat = (double)json_response_body["coord"]["lat"]
    //                     }
    //             );
    //             await _context.SaveChangesAsync();

    //         }
            
    //         //then add weather condition to db
    //         City = await _context.City.SingleAsync(c=> c.Name == Search_String);
    //         if(!await _context.Weather.AnyAsync(c => c.Dt == (int)json_response_body["dt"])){
    //             await _context.Weather.AddAsync(
    //             new Weather {
    //                 Condition = (string)json_response_body["weather"][0]["main"],
    //                 Description = (string)json_response_body["weather"][0]["description"],
    //                 Temp =  (double)json_response_body["main"]["temp"],
    //                 Temp_min = (double)json_response_body["main"]["temp_min"],
    //                 Temp_max = (double)json_response_body["main"]["temp_max"],
    //                 Dt = (int)json_response_body["dt"],
    //                 Timezone = (int)json_response_body["timezone"],
    //                 Lon = City.Lon,
    //                 Lat = City.Lat
    //                 }
    //             );
    //             await _context.SaveChangesAsync();
    //             TempData["Search_String"] = Search_String;
    //         }

    //         Weather = await _context.Weather.SingleAsync
    //         (
    //             w=> w.Lon == City.Lon && w.Lat == City.Lat && w.Dt == (int)json_response_body["dt"] 
    //         );
            
    //     }
    //     catch(Exception e){
    //         Console.WriteLine(e.Message);
    //         TempData["status_code_ok"] = "not_ok";
    //         return RedirectToPage("Index");
    //     }
    //     return Page();

    // }
}
