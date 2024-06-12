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
            _ => null,
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
