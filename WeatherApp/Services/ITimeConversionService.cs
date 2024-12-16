
public interface ITimeConversionService
{
    public DateTime ConvertUnixToDateTime( long unixtime, string timezone);
    
}