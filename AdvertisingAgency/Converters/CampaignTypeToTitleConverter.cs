using AdvertisingAgency.Domain;
using System.Globalization;

namespace AdvertisingAgency.Converters;

public sealed class CampaignTypeToTitleConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            CampaignType.InSearchNetwork => "В поисковой сети",
            CampaignType.DisplayNetwork => "КМС",
            CampaignType.Shopping => "Торговая кампания",
            CampaignType.App => "Приложение",
            CampaignType.Smart => "Умная",
            _ => value,
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
