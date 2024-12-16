using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;

namespace WeatherApp.Models;

//[Index(nameof(Dt), nameof(Timezone),  nameof(Lon),  nameof(Lat), IsUnique = true)]
[Index(nameof(Dt), nameof(Lon),  nameof(Lat), IsUnique = true)]
[PrimaryKey(nameof(Lon), nameof(Lat), nameof(Dt))]

public class Weather
{
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //[Key]
    //public int WeatherId { get; set; }
    public string Condition { get; set; }
    public string Description { get; set; }
    public double Temp { get; set; }
    public double Temp_min { get; set; }
    public double Temp_max { get; set; }
    public int Dt {get; set; }

    public int Timezone { get; set; }
    //[ForeignKey("City"), Column(Order = 0)]
    public double Lon { get; set; }
    //[ForeignKey("City"), Column(Order = 1)]
    public double Lat { get; set; }
    [JsonIgnore]
    public virtual City City{ get; set; }

    //[ForeignKey("City")]
    //public int CityId { get; set; }
    //[ForeignKey("CityId")]
    //public virtual City City { get; set; }
    // [ForeignKey("City")]
    // public int CityFK { get; set; }
    
}