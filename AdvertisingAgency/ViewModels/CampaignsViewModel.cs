using System.Collections.ObjectModel;
using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AdvertisingAgency.ViewModels.CreateCampaign;
using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace AdvertisingAgency.ViewModels;

public sealed partial class CampaignsViewModel : BaseViewModel
{
    private readonly IIdentityService _identityService;
    
    [ObservableProperty] private ObservableCollection<Campaign> _campaigns = [];
    [ObservableProperty] private bool _isRefreshing;
    
    public CampaignsViewModel(IMediator mediator, IIdentityService identityService) : base(mediator)
    {
        _identityService = identityService;
        Refresh(CancellationToken.None).SafeFireAndForget();
    }

    [RelayCommand]
    private async Task Refresh(CancellationToken cancellationToken)
    {
        ClientId id = _identityService.CurrentUser!.Client.Id;
        await UpdateCollectionAsync(Campaigns, new GetCampaignsQuery(id), cancellationToken).ConfigureAwait(false);
        IsRefreshing = false;
    }

    [RelayCommand]
    private Task CreateCampaign(CancellationToken cancellationToken) => 
        Shell.Current.GoToAsync(nameof(ChooseCampaignGoalViewModel))
            .WaitAsync(cancellationToken);
}