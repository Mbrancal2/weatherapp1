using WeatherApp.Models;
using Microsoft.EntityFrameworkCore;
namespace WeatherApp.Data
{
    public class WeatherAppContext : DbContext
    {
        public WeatherAppContext (DbContextOptions<WeatherAppContext> options)
        : base(options)
        {
        }
        public DbSet<City> City { get; set;}
        public DbSet<Weather> Weather { get; set;}

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<City>()
                //.HasMany(e => e.Weathers)
                //.WithOne(e => e.City)
                //.HasIndex(e => new{e.Lat, e.Lon});
                //.HasForeignKey(e =>new {e.Weather_Lat, e.Weather_Lon});
            

            // modelBuilder.Entity<Weather>()
            //     .HasOne( w => w.City )
            //     .WithMany(c => c.Weathers)
            //     .HasForeignKey( w => w.City.)
            //add a unique composite constrait so that you cant add the same city twice. 
            //modelBuilder.Entity<City>()
               // .HasAlternateKey(c => new { c.Lon, c.Lat });
                //.HasIndex(c => new {c.Lat, c.Lon}).IsUnique()
                
            //.HasIndex(c => new {c.Lat, c.Lon}).IsUnique()
            //.HasAlternateKey();
            //modelBuilder.Entity<Weather>()
            //.HasOne(w => w.City)
            //.WithMany(c => c.Weathers)
            //.HasForeignKey(w2 => new {w2.Lat, w2.Lon});
        }

    }
}
