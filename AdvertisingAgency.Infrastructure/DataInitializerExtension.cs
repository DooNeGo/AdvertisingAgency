using AdvertisingAgency.Domain;

namespace AdvertisingAgency.Infrastructure;

internal static class DataInitializerExtension
{
    public static void LoadData(this ApplicationContext context)
    {
        if (context.Users.Any()) return;

        //CampaignGoal[] goals =
        //[
        //    new CampaignGoal("Продажи",
        //        "Увеличить объем продаж на сайте, в приложении, по телефону и в обычном магазине."),
        //    new CampaignGoal("Потенциальные клиенты",
        //        "Найти потенциальных клиентов и получить другие конверсии, побуждая пользователей к действию"),
        //    new CampaignGoal("Трафик сайта", "Привелечь потенциальных клиентов на сайт."),
        //    new CampaignGoal("Интерес к бренду и товарам",
        //        "Привелечь внимание потенциальных клиетов к вашим товаром и услугам."),
        //    new CampaignGoal("Узнаваемость бренда и охват",
        //        "Охватить широкую аудиторию и повысить узнаваемость бренда."),
        //    new CampaignGoal("Реклама приложения",
        //        "Увеличить количество установок приложения, взаимодействия с ним и предварительных регистраций."),
        //    new CampaignGoal("Посещения магазинов и промоакции",
        //        "Привлекать посетителей в магазины, рестораны и дилерские центры."),
        //    new CampaignGoal("Не указывать цель",
        //        "Сначала выбрать тип кампании – без рекомендаций на основе вашей цели.")
        //];

        //CampaignType[] types =
        //[
        //    new CampaignType("В поисковой сети", "Показывайте объявления в поиске заинтересованным пользователям."),
        //    new CampaignType("КМС",
        //        "Показывайте привлекательные объявления на сайтах и в приложениях – наша сеть состоит их трех миллионов ресурсов."),
        //    new CampaignType("Торговая кампания", "Рекламируйте свои товары пользователям, которые ищут, что купить."),
        //    new CampaignType("Приложение", "Привлекайте внимание к своему приложению и увеличивайте число установок."),
        //    new CampaignType("Умная", "Привлекайте клиентов с помощью универсального решения для малого бизнеса.")
        //];

        AdSchedule[] adSchedules =
        [
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0))),
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0))),
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0))),
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0))),
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0))),
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0))),
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0))),
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0)))
        ];

        CampaignSettings[] settings =
        [
            new CampaignSettings(1000, [Country.Russia], [Language.Russian], [adSchedules[0]]),
            new CampaignSettings(2500, [Country.Belarus], [Language.English], [adSchedules[1]]),
            new CampaignSettings(800, [Country.Kazakhstan], [Language.Russian], [adSchedules[2]]),
            new CampaignSettings(1240, [Country.Kyrgyzstan], [Language.English], [adSchedules[3]]),
            new CampaignSettings(3245, [Country.Moldova, Country.Uzbekistan], [Language.English, Language.Russian] , [adSchedules[4]]),
            new CampaignSettings(871, [Country.Tajikistan], [Language.English], [adSchedules[5]]),
            new CampaignSettings(555, [Country.Armenia, Country.Kazakhstan], [Language.English], [adSchedules[6]]),
            new CampaignSettings(4353, [Country.Kyrgyzstan, Country.Uzbekistan], [Language.Russian], [adSchedules[7]])
        ];

        var client = new Client("Белакт", "+375447452007", new FullName("Матвей", "Кострома"), Country.Belarus);
        var user = new User("qwerty", "123123123", client);

        Employee[] employees =
        [
            new Employee(new FullName("Кирилл", "Пархоменко"), "+375445491424", new Position("Директор")),
            new Employee(new FullName("Полина", "Келасьева"), "+375292354323", new Position("Менеджер по продажам")),
            new Employee(new FullName("Павел", "Грамотеев"), "+375294353295", new Position("Аналитик")),
        ];

        Campaign[] campaigns =
        [
            new Campaign(client.Id, employees[^1].Id, CampaignGoal.AppPromotion, CampaignType.App, settings[0], "Молоко") { Status = CampaignStatus.Executing },
            new Campaign(client.Id, employees[^1].Id, CampaignGoal.BrandAwareness, CampaignType.Smart, settings[1], "Сайт") { Status = CampaignStatus.Denied },
            new Campaign(client.Id, employees[^1].Id, CampaignGoal.PotentialClients, CampaignType.DisplayNetwork, settings[2], "Приложение") { Status = CampaignStatus.Completed },
            new Campaign(client.Id, employees[^1].Id, CampaignGoal.BrandInterest, CampaignType.InSearchNetwork, settings[3], "Книга") { Status = CampaignStatus.Executing },
            new Campaign(client.Id, employees[^1].Id, CampaignGoal.WebsiteTraffic, CampaignType.Shopping, settings[4], "Фильм") { Status = CampaignStatus.Executing },
            new Campaign(client.Id, employees[^1].Id, CampaignGoal.Sales, CampaignType.Smart, settings[5], "Музыка") { Status = CampaignStatus.Executing },
            new Campaign(client.Id, employees[^1].Id, CampaignGoal.NoSpecificGoal, CampaignType.DisplayNetwork, settings[6], "Детское питание") { Status = CampaignStatus.Denied },
            new Campaign(client.Id, employees[^1].Id, CampaignGoal.StoreVisits, CampaignType.Shopping, settings[7], "Спортивное питание") { Status = CampaignStatus.Executing }
        ];

        context.AdSchedules.AddRange(adSchedules);
        context.CampaignSettings.AddRange(settings);
        context.Users.Add(user);
        context.Employees.AddRange(employees);
        context.Campaigns.AddRange(campaigns);
        context.SaveChanges();
    }
}