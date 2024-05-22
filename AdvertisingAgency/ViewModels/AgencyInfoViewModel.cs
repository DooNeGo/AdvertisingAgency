using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using Mediator;

namespace AdvertisingAgency.ViewModels;

public sealed partial class AgencyInfoViewModel : ObservableObject
{
    [ObservableProperty] private List<Employee> _employees = [];

    public AgencyInfoViewModel(IMediator mediator)
    {
        Task.Run(async () => Employees = await mediator.Send(new GetEmployeesQuery()));
    }
}