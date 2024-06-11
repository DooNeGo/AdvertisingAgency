using System.Collections.ObjectModel;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace AdvertisingAgency.ViewModels;

public sealed partial class AgencyInfoViewModel : BaseViewModel
{
    [ObservableProperty] private ObservableCollection<Employee> _employees = [];
    [ObservableProperty] private bool _isRefreshing;

    public AgencyInfoViewModel(IMediator mediator) : base(mediator) =>
        RefreshAsync().SafeFireAndForget();

    [RelayCommand]
    private async Task RefreshAsync(CancellationToken cancellationToken = default)
    {
        await UpdateCollectionAsync(Employees, new GetEmployeesQuery(), cancellationToken).ConfigureAwait(false);
        IsRefreshing = false;
    }
}