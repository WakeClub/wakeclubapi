namespace Wakeclub.Services;

public class DateTimeComparison
{
    public static bool IsCurrentTimeBetween8PMAnd12AM()
    {
        DateTime currentTime = DateTime.Now;
        TimeSpan startTime = TimeSpan.FromHours(20); // 8 PM
        TimeSpan endTime = TimeSpan.FromHours(24);   // 12 AM (midnight)

        TimeSpan currentTimeOfDay = currentTime.TimeOfDay;

        return currentTimeOfDay >= startTime && currentTimeOfDay <= endTime;
    }
}
