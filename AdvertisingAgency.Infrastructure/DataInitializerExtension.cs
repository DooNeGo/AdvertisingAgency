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
            new AdSchedule(
                DayOfWeek.Monday,
                DayOfWeek.Friday,
                new TimeOnly(9, 0),
                new TimeOnly(20, 0))
        ];

        Language[] languages =
        [
            new Language("Русский"),
            new Language("Английский"),
            new Language("Белорусский")
        ];

        CampaignSettings[] settings =
        [
            new CampaignSettings([..locations], [languages[0]], [adSchedules[0]]),
            new CampaignSettings([locations[0]], [languages[1]], [adSchedules[0]]),
            new CampaignSettings([locations[1]], [languages[2]], [adSchedules[0]]),
            new CampaignSettings([locations[2]], [languages[0]], [adSchedules[0]]),
            new CampaignSettings([locations[0], locations[1]], [languages[1], languages[2]], [adSchedules[0]]),
            new CampaignSettings([locations[3]], [languages[1]], [adSchedules[0]]),
            new CampaignSettings([locations[3], locations[2]], [languages[1]], [adSchedules[0]]),
            new CampaignSettings([locations[1], locations[2]], [languages[0]], [adSchedules[0]])
        ];
        
        var client = new Client("Белакт", "+375447452007", new FullName("Матвей", "Кострома"), locations[0]);
        var user = new User("qwerty", "123123123", client);
        var position = new Position("Аналитик");
        var employee = new Employee(new FullName("Давид", "Тарасенко"), "+375444682390", position);

        Campaign[] campaigns =
        [
            new Campaign(client, employee, goals[0], types[0], settings[0], "Молоко", 1000),
            new Campaign(client, employee, goals[1], types[1], settings[1],"Сайт", 2500),
            new Campaign(client, employee, goals[2], types[3], settings[2],"Приложение", 800),
            new Campaign(client, employee, goals[3], types[4], settings[3],"Книга", 1240),
            new Campaign(client, employee, goals[4], types[0], settings[4],"Фильм", 3245),
            new Campaign(client, employee, goals[5], types[1], settings[5],"Музыка", 871),
            new Campaign(client, employee, goals[6], types[2], settings[6],"Детское питание", 555),
            new Campaign(client, employee, goals[7], types[3], settings[7],"Спортивное питание", 4353)
        ];
        
        // context.CampaignGoals.AddRange(goals);
        // context.CampaignTypes.AddRange(types);
        // context.Locations.AddRange(locations);
        // context.AdSchedules.AddRange(adSchedules);
        // context.Languages.AddRange(languages);
        // context.CampaignSettings.AddRange(settings);
        context.Users.Add(user);
        context.Campaigns.AddRange(campaigns);
        context.SaveChanges();
    }
}