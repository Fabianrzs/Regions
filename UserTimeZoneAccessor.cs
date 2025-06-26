namespace Regiones;

public interface IUserTimeZoneAccessor
{
    TimeZoneInfo TimeZone { get; }
}

public class UserTimeZoneAccessor(IHttpContextAccessor httpContextAccessor) : IUserTimeZoneAccessor
{
    public TimeZoneInfo TimeZone
    {
        get
        {
            var context = httpContextAccessor.HttpContext;
            if (context?.Items.TryGetValue("UserTimeZone", out var tzObj) == true &&
                tzObj is TimeZoneInfo tz)
            {
                return tz;
            }

            return TimeZoneInfo.Utc;
        }
    }
}