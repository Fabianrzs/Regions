namespace Regiones;

public readonly struct UserDateTime(DateTimeOffset utcDateTime)
{
    private readonly DateTimeOffset _utcDateTime = utcDateTime;
    private static IUserTimeZoneAccessor? _accessor;

    public static void Configure(IUserTimeZoneAccessor accessor) => _accessor = accessor;

    public DateTime LocalDateTime =>
        _accessor == null ? _utcDateTime.UtcDateTime : TimeZoneInfo.ConvertTime(_utcDateTime, _accessor.TimeZone).DateTime;

    public override string ToString() => LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss");
}
