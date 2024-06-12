using System.Globalization;
using AdvertisingAgency.Domain;

namespace AdvertisingAgency.Converters;

public sealed class CampaignStatusConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            CampaignStatus.Executing => "Выполняется",
            CampaignStatus.Reviewing => "Рассматривается",
            CampaignStatus.Completed => "Выполнен",
            CampaignStatus.Denied => "Отказ",
            _ => null
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}