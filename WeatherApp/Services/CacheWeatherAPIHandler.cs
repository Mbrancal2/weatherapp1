using System.Net;
using System.Web;
using Azure.Core;
using Microsoft.Extensions.Caching.Memory;

namespace WeatherApp.Services;
public class CacheWeatherAPIHandler : DelegatingHandler
{
    private readonly IMemoryCache _cache;

    public CacheWeatherAPIHandler(IMemoryCache cache)
    {
        _cache = cache;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var querystring = HttpUtility.ParseQueryString(request.RequestUri!.Query);
        var query = querystring["q"];
        var key = $"{query}";

        var cached = _cache.Get<string>(key);
        //var result = new StringContent(cached);
        if (cached is not null)
        {
            var result = new StringContent(cached);
            return new HttpResponseMessage()
            {
             StatusCode = HttpStatusCode.OK,
              Content = new StringContent(cached)
            };
        }

        var response = await base.SendAsync(request, cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        _cache.Set(key, content, TimeSpan.FromHours(1));
        
        return response;
    }

}