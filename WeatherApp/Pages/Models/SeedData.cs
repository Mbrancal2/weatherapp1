using Microsoft.EntityFrameworkCore;
using WeatherApp.Data;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WeatherApp.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new WeatherAppContext(
            serviceProvider.GetRequiredService<DbContextOptions<WeatherAppContext>>()))
            {
               if(context == null || context.City == null || context.Weather == null){
                    throw new ArgumentException("Null WeatherAppContext");
               }

               if(context.City.Any()){
                    foreach(var city in context.City)
                    {
                        context.City.Remove(city);
                    }
                    context.SaveChanges();
               }
               if(context.Weather.Any()){
                    foreach(var weather in context.Weather)
                    {
                        context.Weather.Remove(weather);
                    }
                    context.SaveChanges();
               }
               
               //remove any perviousely inputted cities.
            //    if(context.City.Any()){
            //         foreach (var city in context.City)
            //         {
            //             context.City.Remove(city);
            //         }
            //         context.SaveChanges();
            //    }

               string string_obj;
               try {
                    StreamReader s = new StreamReader("/home/mbrancal/projects_python/pro3/weatherdata.txt");
                    //for storing the entries in the text file.
                    //for use in adding the weather data to the database.
                    List<JObject> api_calls = new List<JObject>();
                    string_obj = s.ReadLine();
                    while( string_obj != null){
                        
                        //for testing purposes print line to console:
                        //Console.WriteLine(string_obj);
                        string_obj = s.ReadLine();
                        JObject j_obj = JObject.Parse(string_obj);
                        api_calls.Add(j_obj);
                        //add city if city does not alredy exist:
                        string city_name = (string)j_obj["name"];
                        //Console.WriteLine(j_obj["coord"]["lon"]);
                        double city_lon = (double)j_obj["coord"]["lon"];
                        double city_lat = (double) j_obj["coord"]["lat"];
                        string weather_condition = (string)j_obj["weather"][0]["main"];
                        string weather_description = (string)j_obj["weather"][0]["description"];
                        double weather_temp = (double)j_obj["main"]["temp"];
                        double weather_temp_min = (double)j_obj["main"]["temp_min"];
                        double weather_temp_max = (double)j_obj["main"]["temp_max"];
                        int weather_dt = (int)j_obj["dt"];
                        int weather_timezone = (int)j_obj["timezone"];
                        //{'coord': {'lon': -123.1193, 'lat': 49.2497}, 'weather': [{'id': 500, 'main': 'Rain', 'description': 'light rain', 'icon': '10d'}], 'base': 'stations', 'main': {'temp': 284.23, 'feels_like': 283.48, 'temp_min': 283.29, 'temp_max': 284.98, 'pressure': 1011, 'humidity': 80}, 'visibility': 10000, 'wind': {'speed': 4.63, 'deg': 100}, 'rain': {'1h': 0.43}, 'clouds': {'all': 100}, 'dt': 1716563624, 'sys': {'type': 2, 'id': 2011597, 'country': 'CA', 'sunrise': 1716553089, 'sunset': 1716609657}, 'timezone': -25200, 'id': 6173331, 'name': 'Vancouver', 'cod': 200}
                        if(!context.City.Any(c => c.Lon == city_lon && c.Lat == city_lat)){
                            context.City.Add(
                                new City {
                                    Name = city_name,
                                   Lat = city_lat,
                                   Lon = city_lon
                                }
                            );
                        }
                        context.SaveChanges();
                        if(!context.Weather.Any(w=> w.Dt == weather_dt && w.Timezone == weather_timezone)){
                            context.Weather.Add(
                                new Weather {
                                    Condition = weather_condition,
                                    Description = weather_description,
                                    Temp = weather_temp,
                                    Temp_min = weather_temp_min,
                                    Temp_max = weather_temp_max,
                                    Dt = weather_dt,
                                    Timezone = weather_timezone,
                                    Lon = city_lon,
                                    Lat = city_lat
                                }
                            );
                        }
                        context.SaveChanges();


                        //add weather if not alredy exists:

                    }
                    s.Close();
                    foreach(JObject entry in api_calls){
                        //add in entry to the weather table.
                    }

               }
               catch(Exception e)
               {
                    Console.WriteLine("Exception: " + e.Message);
               }

            //    if (context.Weather.Any() && context.City.Any()){
            //         return;
            //    }

            
               //context.Database.ExecuteSqlRaw("TRUNCATE TABLE CITY");
               //ontext.Database.ExecuteSqlRaw("truncate table CITY");
               //context.Database.ExecuteSqlRaw("TRUNCATE TABLE CITY");
            //    if (context.City.Any()){
            //         context.City.AddRange(
            //             new City{
            //                 Name = "London",
            //                 Lon = -0.1257,
            //                 Lat = 51.5085

            //             },
            //             new City{
            //                 Name = "Ann Arbor",
            //                 Lon = -83.7409,
            //                 Lat = 42.2776
            //             },
            //             new City{
            //                 Name = "Detroit",
            //                 Lon = -83.0458,
            //                 Lat = 42.3314
            //             },
            //             new City{
            //                 Name = "New York",
            //                 Lon = -74.006,
            //                 Lat = 40.7143
            //             },
            //             new City{
            //                 Name = "Boston",
            //                 Lon = -71.0598,
            //                 Lat = 42.3584
            //             },
            //             new City{
            //                 Name = "Atlanta",
            //                 Lon = -84.388,
            //                 Lat = 33.749
            //             },
            //             new City{
            //                 Name = "San Francisco",
            //                 Lon = -122.4194,
            //                 Lat = 37.7749
            //             },
            //             new City{
            //                 Name = "Los Angeles",
            //                 Lon = -118.2437,
            //                 Lat = 34.0522
            //             },
            //             new City{
            //                 Name = "Chicago",
            //                 Lon = -87.65,
            //                 Lat = 41.85
            //             },
            //             new City{
            //                 Name = "Austin",
            //                 Lon = -97.7431,
            //                 Lat = 30.2672
            //             }
            //         );
            //         context.SaveChanges();
            //    }
            //    if (!context.City.Any()){
            //         context.City.AddRange(
            //             new City{
            //                 Name = "London",
            //                 Lon = -0.1257,
            //                 Lat = 51.5085

            //             },
            //             new City{
            //                 Name = "Ann Arbor",
            //                 Lon = -83.7409,
            //                 Lat = 42.2776
            //             },
            //             new City{
            //                 Name = "Detroit",
            //                 Lon = -83.0458,
            //                 Lat = 42.3314
            //             },
            //             new City{
            //                 Name = "New York",
            //                 Lon = -74.006,
            //                 Lat = 40.7143
            //             },
            //             new City{
            //                 Name = "Boston",
            //                 Lon = -71.0598,
            //                 Lat = 42.3584
            //             },
            //             new City{
            //                 Name = "Atlanta",
            //                 Lon = -84.388,
            //                 Lat = 33.749
            //             },
            //             new City{
            //                 Name = "San Francisco",
            //                 Lon = -122.4194,
            //                 Lat = 37.7749
            //             },
            //             new City{
            //                 Name = "Los Angeles",
            //                 Lon = -118.2437,
            //                 Lat = 34.0522
            //             },
            //             new City{
            //                 Name = "Chicago",
            //                 Lon = -87.65,
            //                 Lat = 41.85
            //             },
            //             new City{
            //                 Name = "Austin",
            //                 Lon = -97.7431,
            //                 Lat = 30.2672
            //             }
            //         );
            //         context.SaveChanges();
            //    }
               //if(!context.Weather.Any()){
                    //var cities = context.City.Select(x=>x.CityId).ToList();
                    //var cities = from c in context.City
                                //select c.CityId;
                    //context.City.FromSql($"SELECT CityId FROM City").ToList();
                    // var ids = context.City.
                    //FromSql($"SELECT CityId FROM City")
                    //     .OrderBy(x => x.CityId)
                    //     .ToList();
                    
                    // context.Weather.AddRange(
                    //     //London
                    //     new Weather{
                    //         Condition = "rain",
                    //         Description = "light rain",
                    //         Temp = 281.91,
                    //         Temp_min = 281.24,
                    //         Temp_max = 283.02,
                    //         CityId = ids[0].CityId
                    //     },
                    //     //Ann Arbor
                    //      new Weather{
                    //         Condition = "Clouds",
                    //         Description = "broken clouds",
                    //         Temp = 297.7,
                    //         Temp_min = 296.03,
                    //         Temp_max = 298.81,
                    //         CityId = ids[1].CityId
                    //     },
                    //     //Detroit
                    //     new Weather{
                    //         Condition = "Clouds",
                    //         Description = "scattered clouds",
                    //         Temp = 299.55,
                    //         Temp_min = 295.23,
                    //         Temp_max = 300.35,
                    //         CityId = ids[2].CityId
                    //     },
                    //     //New York
                    //     new Weather{
                    //         Condition = "Clouds",
                    //         Description = "broken clouds",
                    //         Temp = 300.89,
                    //         Temp_min = 297.01,
                    //         Temp_max = 304.8,
                    //         CityId = ids[3].CityId
                    //     },
                    //     //Boston
                    //     new Weather{
                    //         Condition = "Clouds",
                    //         Description = "overcast clouds",
                    //         Temp = 290,
                    //         Temp_min = 284.91,
                    //         Temp_max = 297.06,
                    //         CityId = ids[4].CityId
                    //     },
                    //     //Atlanta
                    //     new Weather{
                    //         Condition = "Clouds",
                    //         Description = "broken clouds",
                    //         Temp = 297.26,
                    //         Temp_min = 295.75,
                    //         Temp_max = 298.59,
                    //         CityId = ids[5].CityId
                    //     },
                    //     //San Francisco
                    //      new Weather{
                    //         Condition = "Clear",
                    //         Description = "clear sky",
                    //         Temp = 291.57,
                    //         Temp_min = 287.93,
                    //         Temp_max = 296.26,
                    //         CityId = ids[6].CityId
                    //     },
                    //     //Los Angles
                    //     new Weather{
                    //         Condition = "Fog",
                    //         Description = "fog",
                    //         Temp = 295.53,
                    //         Temp_min = 288.94,
                    //         Temp_max = 302.36,
                    //         CityId = ids[7].CityId
                    //     },
                    //     //Chicago
                    //     new Weather{
                    //         Condition = "Clouds",
                    //         Description = "overcast clouds",
                    //         Temp = 292.06,
                    //         Temp_min = 291.05,
                    //         Temp_max = 293.03,
                    //         CityId = ids[8].CityId
                    //     },
                    //     //Austin
                    //     new Weather{
                    //         Condition = "Clear",
                    //         Description = "clear sky",
                    //         Temp = 303.97,
                    //         Temp_min = 302.14,
                    //         Temp_max = 305.71,
                    //         CityId = ids[9].CityId
                    //     }
                        
                    // );
               //}
            }
    }

}