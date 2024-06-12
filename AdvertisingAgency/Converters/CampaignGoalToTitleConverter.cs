using AdvertisingAgency.Domain;
using System.Globalization;

namespace AdvertisingAgency.Converters;

public sealed class CampaignGoalToTitleConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            CampaignGoal.Sales => "Продажи",
            CampaignGoal.PotentialClients => "Потенциальные клиенты",
            CampaignGoal.WebsiteTraffic => "Трафик сайта",
            CampaignGoal.BrandInterest => "Интерес к бренду и товарам",
            CampaignGoal.BrandAwareness => "Узнаваемость бренда и охват",
            CampaignGoal.AppPromotion => "Реклама приложения",
            CampaignGoal.StoreVisits => "Посещения магазинов и промоакции",
            CampaignGoal.NoSpecificGoal => "Не указывать цель",
            _ => value,
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
