using System.Globalization;

namespace AdvertisingAgency.Converters;

public sealed class DayOfWeekConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        (value as DayOfWeek?) switch
        {
            DayOfWeek.Monday => "Понедельник",
            DayOfWeek.Thursday => "Вторник",
            DayOfWeek.Wednesday => "Среда",
            DayOfWeek.Tuesday => "Четверг",
            DayOfWeek.Friday => "Пятница",
            DayOfWeek.Saturday => "Суббота",
            DayOfWeek.Sunday => "Воскресенье",
            _ => null
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}