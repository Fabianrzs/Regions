namespace Regions;

internal class TimeZoneMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var timeZoneId = context.Request.Headers["X-Time-Zone"].FirstOrDefault();

        if (!string.IsNullOrWhiteSpace(timeZoneId))
        {
            try
            {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                context.Items["UserTimeZone"] = timeZone;
            }
            catch (TimeZoneNotFoundException)
            {
                context.Items["UserTimeZone"] = TimeZoneInfo.Utc;
            }
        }
        else
        {
            context.Items["UserTimeZone"] = TimeZoneInfo.Utc;
        }

        await _next(context);
    }
}
