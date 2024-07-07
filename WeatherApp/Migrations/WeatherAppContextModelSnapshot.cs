﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WeatherApp.Data;

#nullable disable

namespace WeatherApp.Migrations
{
    [DbContext(typeof(WeatherAppContext))]
    partial class WeatherAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("WeatherApp.Models.City", b =>
                {
                    b.Property<double>("Lon")
                        .HasColumnType("REAL");

                    b.Property<double>("Lat")
                        .HasColumnType("REAL");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Lon", "Lat");

                    b.ToTable("City");
                });

            modelBuilder.Entity("WeatherApp.Models.Weather", b =>
                {
                    b.Property<double>("Lon")
                        .HasColumnType("REAL");

                    b.Property<double>("Lat")
                        .HasColumnType("REAL");

                    b.Property<int>("Dt")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Condition")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Temp")
                        .HasColumnType("REAL");

                    b.Property<double>("Temp_max")
                        .HasColumnType("REAL");

                    b.Property<double>("Temp_min")
                        .HasColumnType("REAL");

                    b.Property<int>("Timezone")
                        .HasColumnType("INTEGER");

                    b.HasKey("Lon", "Lat", "Dt");

                    b.HasIndex("Dt", "Lon", "Lat")
                        .IsUnique();

                    b.ToTable("Weather");
                });

            modelBuilder.Entity("WeatherApp.Models.Weather", b =>
                {
                    b.HasOne("WeatherApp.Models.City", "City")
                        .WithMany("Weathers")
                        .HasForeignKey("Lon", "Lat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("WeatherApp.Models.City", b =>
                {
                    b.Navigation("Weathers");
                });
#pragma warning restore 612, 618
        }
    }
}
