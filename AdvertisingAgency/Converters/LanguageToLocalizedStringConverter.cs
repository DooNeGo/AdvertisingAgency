using AdvertisingAgency.Domain;
using System.Globalization;

namespace AdvertisingAgency.Converters;

public sealed class LanguageToLocalizedStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            Language.Russian => "Русский",
            Language.English => "Английский",
            Language.Belarusian => "Белорусский",
            _ => value
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            "Русский" => Language.Russian,
            "Английский" => Language.English,
            "Белорусский" => Language.Belarusian,
            _ => value
        };
}
