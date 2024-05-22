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
            new Location("Украина"),
            new Location("Латвия")
        ];

        AdSchedule[] adSchedules =
        [
            new AdSchedule(DayOfWeek.Monday, TimeSpan.FromHours(9), TimeSpan.FromHours(20))
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
            new CampaignSettings(2500, [locations[0]], [languages[1]], [adSchedules[0]]),
            new CampaignSettings(800, [locations[1]], [languages[2]], [adSchedules[0]]),
            new CampaignSettings(1240, [locations[2]], [languages[0]], [adSchedules[0]]),
            new CampaignSettings(3245, [locations[0], locations[1]], [languages[1], languages[2]], [adSchedules[0]]),
            new CampaignSettings(871, [locations[3]], [languages[1]], [adSchedules[0]]),
            new CampaignSettings(555, [locations[3], locations[2]], [languages[1]], [adSchedules[0]]),
            new CampaignSettings(4353, [locations[1], locations[2]], [languages[0]], [adSchedules[0]])
        ];
        
        var client = new Client("Белакт", "+375447452007", new FullName("Матвей", "Кострома"), locations[0]);
        var user = new User("qwerty", "123123123", client);

        Employee[] employees =
        [
            new Employee(new FullName("Кирилл", "Пархоменко"), "+375445491424", new Position("Директор")),
            new Employee(new FullName("Полина", "Келасьева"), "+375292354323", new Position("Менеджер по продажам")),
            new Employee(new FullName("Павел", "Грамотеев"), "+375294353295", new Position("Аналитик")),
        ];

        Campaign[] campaigns =
        [
            new Campaign(client, employees[^1], goals[0], types[0], settings[0], "Молоко"),
            new Campaign(client, employees[^1], goals[1], types[1], settings[1],"Сайт"),
            new Campaign(client, employees[^1], goals[2], types[3], settings[2],"Приложение"),
            new Campaign(client, employees[^1], goals[3], types[4], settings[3],"Книга"),
            new Campaign(client, employees[^1], goals[4], types[0], settings[4],"Фильм"),
            new Campaign(client, employees[^1], goals[5], types[1], settings[5],"Музыка"),
            new Campaign(client, employees[^1], goals[6], types[2], settings[6],"Детское питание"),
            new Campaign(client, employees[^1], goals[7], types[3], settings[7],"Спортивное питание")
        ];
        
        // context.CampaignGoals.AddRange(goals);
        // context.CampaignTypes.AddRange(types);
        // context.Locations.AddRange(locations);
        // context.AdSchedules.AddRange(adSchedules);
        // context.Languages.AddRange(languages);
        // context.CampaignSettings.AddRange(settings);
        context.Users.Add(user);
        context.Employees.AddRange(employees);
        context.Campaigns.AddRange(campaigns);
        context.SaveChanges();
    }
}