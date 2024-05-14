using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace AdvertisingAgency.ViewModels;

public sealed partial class CampaignsViewModel : ObservableObject
{
    [ObservableProperty] private List<Campaign> _campaigns = [];
    
    public CampaignsViewModel(IMediator mediator)
    {
        Task.Run(async () => Campaigns = await mediator.Send(new GetCampaignsQuery()));
    }

    [RelayCommand]
    private Task CreateCampaign() => 
        Shell.Current.GoToAsync(nameof(ViewModels.CreateCampaign.ChooseCampaignGoalViewModel));
}