using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace AdvertisingAgency.ViewModels.CreateCampaign;

public sealed partial class ChooseCampaignTypeViewModel : ObservableObject
{
    [ObservableProperty] private List<CampaignType> _campaignTypes = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SetCampaignTypeCommand))]
    private CampaignType? _campaignType;
    
    public ChooseCampaignTypeViewModel(IMediator mediator) =>
        Task.Run(async () => CampaignTypes = await mediator.Send(new GetCampaignTypesQuery()));

    [RelayCommand]
    private void SetCampaignType(CampaignType type) => CampaignType = type;

    [RelayCommand]
    private Task GoNext() => Shell.Current.GoToAsync(nameof(CampaignSettingsViewModel));
}