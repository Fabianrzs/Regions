namespace Regions;

public readonly struct UserDateTime(DateTimeOffset utcDateTime)
{
    private static IUserTimeZoneAccessor? accessor;

    public static void Configure(IUserTimeZoneAccessor accessor) => UserDateTime.accessor = accessor;

    public DateTime LocalDateTime =>
        accessor == null ? utcDateTime.UtcDateTime : TimeZoneInfo.ConvertTime(utcDateTime, accessor.TimeZone).DateTime;

    public override string ToString() => LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss");
}
