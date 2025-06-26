using Regiones;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserTimeZoneAccessor, UserTimeZoneAccessor>();

var app = builder.Build();

app.UseMiddleware<TimeZoneMiddleware>();

app.Use(async (context, next) =>
{
    var accessor = context.RequestServices
    .GetRequiredService<IUserTimeZoneAccessor>();
    UserDateTime.Configure(accessor);
    await next();
});

app.UseHttpsRedirection();


app.MapGet("/api/local/date", () =>
{
    var userDate = new UserDateTime(DateTimeOffset.UtcNow);
    var dateTime = userDate.LocalDateTime;

    return new
    {
        Date = DateOnly.FromDateTime(dateTime),
        Time = TimeOnly.FromDateTime(dateTime).ToString("HH:mm:ss")
    };
})
.WithName("Region");


await app.RunAsync();



