using AdvertisingAgency.Domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AdvertisingAgency.ViewModels;

public sealed partial class AgencyInfoViewModel : ObservableObject
{
    [ObservableProperty] private IEnumerable<Employee> _employees;

    public AgencyInfoViewModel()
    {
        
    }
}