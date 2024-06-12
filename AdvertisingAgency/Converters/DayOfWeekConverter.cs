using System.Globalization;

namespace AdvertisingAgency.Converters;

public sealed class DayOfWeekConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            DayOfWeek.Monday => "Понедельник",
            DayOfWeek.Tuesday => "Вторник",
            DayOfWeek.Wednesday => "Среда",
            DayOfWeek.Thursday => "Четверг",
            DayOfWeek.Friday => "Пятница",
            DayOfWeek.Saturday => "Суббота",
            DayOfWeek.Sunday => "Воскресенье",
            _ => value
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            "Понедельник" => DayOfWeek.Monday,
            "Вторник" => DayOfWeek.Tuesday,
            "Среда" => DayOfWeek.Wednesday,
            "Четверг" => DayOfWeek.Thursday,
            "Пятница" => DayOfWeek.Friday,
            "Суббота" => DayOfWeek.Saturday,
            "Воскресенье" => DayOfWeek.Sunday,
            _ => value
        };
}