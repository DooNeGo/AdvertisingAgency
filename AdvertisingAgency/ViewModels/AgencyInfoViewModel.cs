using AdvertisingAgency.Domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AdvertisingAgency.ViewModels;

public sealed partial class AgencyInfoViewModel : ObservableObject
{
    [ObservableProperty]
    private IEnumerable<Employee> _employees;

    public AgencyInfoViewModel()
    {
        Employees =
        [
                new Employee("Пархоменко Кирилл Дмитриевич", "+375445491424", new DateOnly(2002, 2, 7),
                        new Position("Директор")),
                new Employee("Пархоменко Кирилл Дмитриевич", "+375445491424", new DateOnly(2002, 2, 7),
                        new Position("Менеджер продаж")),
                new Employee("Пархоменко Кирилл Дмитриевич", "+375445491424", new DateOnly(2002, 2, 7),
                        new Position("Аналитик")),
                new Employee("Пархоменко Кирилл Дмитриевич", "+375445491424", new DateOnly(2002, 2, 7),
                        new Position("Аналитик")),
        ];
    }
}