using System.Collections;
using System.Collections.Immutable;
using System.Globalization;

namespace AdvertisingAgency.Converters;

public class CollectionToLocalizedStringsConverter<TElement, TElementConverter> : IValueConverter
    where TElementConverter : IValueConverter, new()
{
    private readonly TElementConverter _converter = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not IEnumerable<TElement> enumerable) return null;
        return targetType switch
        {
            { Name: nameof(IEnumerable) } => ConvertToIEnumerable(enumerable, culture),
            _ => ConvertToList(enumerable, culture),
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not IEnumerable<string> enumerable) return null;
        return enumerable
            .Select(country => _converter.ConvertBack(country, typeof(TElement), null, culture))
            .ToImmutableArray();
    }

    private IEnumerable<object?> ConvertToIEnumerable(IEnumerable<TElement> enumerable, CultureInfo culture) => enumerable
        .Select(country => _converter.Convert(country, typeof(string), null, culture));

    private List<object?> ConvertToList(IEnumerable<TElement> enumerable, CultureInfo culture) =>
        ConvertToIEnumerable(enumerable, culture).ToList();
}