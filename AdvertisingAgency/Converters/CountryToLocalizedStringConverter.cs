using AdvertisingAgency.Domain;
using System.Globalization;

namespace AdvertisingAgency.Converters;

public sealed class CountryToLocalizedStringConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            Country.Armenia => "Армения",
            Country.Belarus => "Беларусь",
            Country.Kazakhstan => "Казахстан",
            Country.Kyrgyzstan => "Кыргызстан",
            Country.Moldova => "Молдова",
            Country.Russia => "Россия",
            Country.Tajikistan => "Таджикистан",
            Country.Uzbekistan => "Узбекистан",
            _ => value,
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        (value as string)?.Trim() switch
        {
            "Армения" => Country.Armenia,
            "Беларусь" => Country.Belarus,
            "Казахстан" => Country.Kazakhstan,
            "Кыргызстан" => Country.Kyrgyzstan,
            "Молдова" => Country.Moldova,
            "Россия" => Country.Russia,
            "Таджикистан" => Country.Tajikistan,
            "Узбекистан" => Country.Uzbekistan,
            _ => value
        };
}
