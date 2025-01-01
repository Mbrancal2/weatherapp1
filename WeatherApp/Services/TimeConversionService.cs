
public class TimeConversionService : ITimeConversionService
{
    public DateTime ConvertUnixToDateTime(long unixtime, int timezone)
    {
        // Convert Unix time to DateTimeOffset in UTC 
        DateTimeOffset datetimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixtime); 
        // Get the UTC DateTime from DateTimeOffset 
        DateTime utcDateTime = datetimeOffset.UtcDateTime; 
        // Add the timezone offset to get the local DateTime
        DateTime localDateTime = utcDateTime.AddSeconds(timezone);
        //Console.WriteLine(unixtime);
        //Console.WriteLine(timezone);
        // Return the local DateTime 
        return localDateTime;
        
    }
    
}
