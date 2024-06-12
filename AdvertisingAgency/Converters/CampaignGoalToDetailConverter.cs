using AdvertisingAgency.Domain;
using System.Globalization;

namespace AdvertisingAgency.Converters;

public sealed class CampaignGoalToDetailConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value switch
        {
            CampaignGoal.Sales => "Увеличить объем продаж на сайте, в приложении, по телефону и в обычном магазине.",
            CampaignGoal.PotentialClients => "Найти потенциальных клиентов и получить другие конверсии, побуждая пользователей к действию",
            CampaignGoal.WebsiteTraffic => "Привелечь потенциальных клиентов на сайт.",
            CampaignGoal.BrandInterest => "Привелечь внимание потенциальных клиетов к вашим товаром и услугам.",
            CampaignGoal.BrandAwareness => "Охватить широкую аудиторию и повысить узнаваемость бренда.",
            CampaignGoal.AppPromotion => "Увеличить количество установок приложения, взаимодействия с ним и предварительных регистраций.",
            CampaignGoal.StoreVisits => "Привлекать посетителей в магазины, рестораны и дилерские центры.",
            CampaignGoal.NoSpecificGoal => "Сначала выбрать тип кампании – без рекомендаций на основе вашей цели.",
            _ => null,
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
