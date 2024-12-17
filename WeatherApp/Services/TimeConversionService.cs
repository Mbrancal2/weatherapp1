
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
        Console.WriteLine(unixtime);
        Console.WriteLine(timezone);
        // Return the local DateTime 
        return localDateTime;
        //adds up given unixtime + number of seconds difference from timezone
        //long total_Seconds = unixtime + (timezone * 60);
         //DateTimeOffset datetimeoffset = DateTimeOffset.FromUnixTimeSeconds(unixtime);
         //DateTime utctim = datetimeoffset.UtcDateTime;

        //DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixtime); 
        //TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timezone); 
        ///DateTime localTime = TimeZoneInfo.ConvertTime(dateTimeOffset.UtcDateTime, timeZone); 
        ///
         //Console.WriteLine(utctim.AddSeconds(timezone));
         //return utctim.AddSeconds(timezone);
    }
    
}