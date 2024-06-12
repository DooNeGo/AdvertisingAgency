using AdvertisingAgency.Domain;
using System.Globalization;

namespace AdvertisingAgency.Converters;

public sealed class CampaignTypeToDetailConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            CampaignType.InSearchNetwork => "Показывайте объявления в поиске заинтересованным пользователям.",
            CampaignType.DisplayNetwork => "Показывайте привлекательные объявления на сайтах и в приложениях – наша сеть состоит их трех миллионов ресурсов.",
            CampaignType.Shopping => "Рекламируйте свои товары пользователям, которые ищут, что купить.",
            CampaignType.App => "Привлекайте внимание к своему приложению и увеличивайте число установок.",
            CampaignType.Smart => "Привлекайте клиентов с помощью универсального решения для малого бизнеса.",
            _ => value,
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
