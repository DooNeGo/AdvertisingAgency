using System.Collections.ObjectModel;
using AdvertisingAgency.Domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AdvertisingAgency.ViewModels;

public sealed partial class RequestsViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Brief> _briefs;

    public RequestsViewModel()
    {
        var client = new Client("Белакт", "Гомель", "Беларусь", "+375447452007", "Кострома Матвей Олегович");
        var position = new Position("Аналитик");
        var mediaplan = new MediaPlan(new DateOnly(2024, 5, 7), new DateOnly(2025, 5, 7),
                [new Service("Продвижение в Instagram", 5000)]);
        var employee = new Employee("Тарасенко Давид Игоревич", "+375444682390", new DateOnly(1990, 2, 3), position);
        Briefs =
        [
                new Brief(client, employee, mediaplan, "Молоко", "Люди +5 лет", 5000, string.Empty,
                        "Повышение продаж"),
                new Brief(client, employee, mediaplan, "Молоко", "Люди +5 лет", 5000, string.Empty,
                "Повышение продаж")
        ];
    }
}