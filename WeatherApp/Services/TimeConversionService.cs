
public class TimeConversionService : ITimeConversionService
{
    public DateTime ConvertUnixToDateTime( long unixtime, string timezone)
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixtime); 
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone); 
        DateTime localTime = TimeZoneInfo.ConvertTime(dateTimeOffset.UtcDateTime, timeZone); 
        return localTime;
    }
    
}