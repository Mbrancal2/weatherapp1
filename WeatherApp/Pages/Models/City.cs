using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Models;
[PrimaryKey(nameof(Lon), nameof(Lat))]
public class City
{
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //[Key]
   // public int CityId { get; set; }
    public string Name { get; set; }

    public double Lon { get; set; }

    public double Lat { get; set; }
    [JsonIgnore]
    public virtual ICollection<Weather> Weathers { get; set; }

}