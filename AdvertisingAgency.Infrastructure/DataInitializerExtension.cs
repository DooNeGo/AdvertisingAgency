using AdvertisingAgency.Domain;

namespace AdvertisingAgency.Infrastructure;

internal static class DataInitializerExtension
{
    public static void LoadData(this ApplicationContext context)
    {
        if (context.Languages.Any()) return;

        CampaignGoal[] goals =
        [
            new CampaignGoal("Продажи",
                "Увеличить объем продаж на сайте, в приложении, по телефону и в обычном магазине."),
            new CampaignGoal("Потенциальные клиенты",
                "Найти потенциальных клиентов и получить другие конверсии, побуждая пользователей к действию"),
            new CampaignGoal("Трафик сайта", "Привелечь потенциальных клиентов на сайт."),
            new CampaignGoal("Интерес к бренду и товарам",
                "Привелечь внимание потенциальных клиетов к вашим товаром и услугам."),
            new CampaignGoal("Узнаваемость бренда и охват",
                "Охватить широкую аудиторию и повысить узнаваемость бренда."),
            new CampaignGoal("Реклама приложения",
                "Увеличить количество установок приложения, взаимодействия с ним и предварительных регистраций."),
            new CampaignGoal("Посещения магазинов и промоакции",
                "Привлекать посетителей в магазины, рестораны и дилерские центры."),
            new CampaignGoal("Не указывать цель",
                "Сначала выбрать тип кампании – без рекомендаций на основе вашей цели.")
        ];

        CampaignType[] types =
        [
            new CampaignType("В поисковой сети", "Показывайте объявления в поиске заинтересованным пользователям."),
            new CampaignType("КМС",
                "Показывайте привлекательные объявления на сайтах и в приложениях – наша сеть состоит их трех миллионов ресурсов."),
            new CampaignType("Торговая кампания", "Рекламируйте свои товары пользователям, которые ищут, что купить."),
            new CampaignType("Приложение", "Привлекайте внимание к своему приложению и увеличивайте число установок."),
            new CampaignType("Умная", "Привлекайте клиентов с помощью универсального решения для малого бизнеса.")
        ];

        Location[] locations =
        [
            new Location("Беларусь"),
            new Location("Россия"),
            new Location("Литва"),
            new Location("Латвия")
        ];

        AdSchedule[] adSchedules =
        [
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0))),
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0))),
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0))),
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0))),new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0))),
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0))),
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0))),
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0))),
            new AdSchedule(DayOfWeek.Monday, new DateTime(DateOnly.MinValue, new TimeOnly(9, 0)), new DateTime(DateOnly.MinValue, new TimeOnly(22, 0)))
        ];

        Language[] languages =
        [
            new Language("Русский"),
            new Language("Английский"),
            new Language("Белорусский")
        ];

        CampaignSettings[] settings =
        [
            new CampaignSettings(1000, [..locations], [languages[0]], [adSchedules[0]]),
            new CampaignSettings(2500, [locations[0]], [languages[1]], [adSchedules[1]]),
            new CampaignSettings(800, [locations[1]], [languages[2]], [adSchedules[2]]),
            new CampaignSettings(1240, [locations[2]], [languages[0]], [adSchedules[3]]),
            new CampaignSettings(3245, [locations[0], locations[1]], [languages[1], languages[2]], [adSchedules[4]]),
            new CampaignSettings(871, [locations[3]], [languages[1]], [adSchedules[5]]),
            new CampaignSettings(555, [locations[3], locations[2]], [languages[1]], [adSchedules[6]]),
            new CampaignSettings(4353, [locations[1], locations[2]], [languages[0]], [adSchedules[7]])
        ];

        var client = new Client("Белакт", "+375447452007", new FullName("Матвей", "Кострома"), locations[0].Id);
        var user = new User("qwerty", "123123123", client);

        Employee[] employees =
        [
            new Employee(new FullName("Кирилл", "Пархоменко"), "+375445491424", new Position("Директор")),
            new Employee(new FullName("Полина", "Келасьева"), "+375292354323", new Position("Менеджер по продажам")),
            new Employee(new FullName("Павел", "Грамотеев"), "+375294353295", new Position("Аналитик")),
        ];

        Campaign[] campaigns =
        [
            new Campaign(client.Id, employees[^1].Id, goals[0].Id, types[0].Id, settings[0], "Молоко"),
            new Campaign(client.Id, employees[^1].Id, goals[1].Id, types[1].Id, settings[1], "Сайт"),
            new Campaign(client.Id, employees[^1].Id, goals[2].Id, types[3].Id, settings[2], "Приложение"),
            new Campaign(client.Id, employees[^1].Id, goals[3].Id, types[4].Id, settings[3], "Книга"),
            new Campaign(client.Id, employees[^1].Id, goals[4].Id, types[0].Id, settings[4], "Фильм"),
            new Campaign(client.Id, employees[^1].Id, goals[5].Id, types[1].Id, settings[5], "Музыка"),
            new Campaign(client.Id, employees[^1].Id, goals[6].Id, types[2].Id, settings[6], "Детское питание"),
            new Campaign(client.Id, employees[^1].Id, goals[7].Id, types[3].Id, settings[7], "Спортивное питание")
        ];

        context.CampaignGoals.AddRange(goals);
        context.CampaignTypes.AddRange(types);
        context.Locations.AddRange(locations);
        context.AdSchedules.AddRange(adSchedules);
        context.Languages.AddRange(languages);
        context.CampaignSettings.AddRange(settings);
        context.Users.Add(user);
        context.Employees.AddRange(employees);
        context.Campaigns.AddRange(campaigns);
        context.SaveChanges();
    }
}