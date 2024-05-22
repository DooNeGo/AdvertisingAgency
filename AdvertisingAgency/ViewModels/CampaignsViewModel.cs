using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace AdvertisingAgency.ViewModels;

public sealed partial class CampaignsViewModel : ObservableObject
{
    [ObservableProperty] private List<Campaign> _campaigns = [];
    
    public CampaignsViewModel(IMediator mediator, IIdentityService identityService)
    {
        Task.Run(async () =>
        {
            ClientId id = identityService.CurrentUser!.Client.Id;
            return Campaigns = await mediator.Send(new GetCampaignsQuery(id));
        });
    }

    [RelayCommand]
    private Task CreateCampaign() => 
        Shell.Current.GoToAsync(nameof(ViewModels.CreateCampaign.ChooseCampaignGoalViewModel));
}