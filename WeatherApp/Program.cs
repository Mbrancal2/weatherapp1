using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Data;
using WeatherApp.Models;
using WeatherApp.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<WeatherAppContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("WeatherAppContext") ?? throw new InvalidOperationException("Connection string 'WeatherAppContext' not found.")));


builder.Services.AddMemoryCache();
builder.Services.AddScoped<CacheWeatherAPIHandler>();
builder.Services.AddSingleton<ITimeConversionService, TimeConversionService>();

builder.Services.AddHttpClient<IWeatherService, WeatherHttpClient>();
//.AddHttpMessageHandler<CacheWeatherAPIHandler>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//builder.Services.AddHttpClient();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
