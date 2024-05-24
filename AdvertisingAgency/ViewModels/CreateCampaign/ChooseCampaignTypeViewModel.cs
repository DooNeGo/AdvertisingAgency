using System.Collections.ObjectModel;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace AdvertisingAgency.ViewModels.CreateCampaign;

public sealed partial class ChooseCampaignTypeViewModel : BaseViewModel
{
    [ObservableProperty] private ObservableCollection<CampaignType> _campaignTypes = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(SetCampaignTypeCommand))]
    private CampaignType? _campaignType;

    public ChooseCampaignTypeViewModel(IMediator mediator) : base(mediator) =>
        UpdateCollectionAsync(CampaignTypes, new GetCampaignTypesQuery(), CancellationToken.None).SafeFireAndForget();

    [RelayCommand]
    private void SetCampaignType(CampaignType type) => CampaignType = type;

    [RelayCommand]
    private Task GoNext(CancellationToken cancellationToken) => 
        Shell.Current.GoToAsync(nameof(CampaignSettingsViewModel)).WaitAsync(cancellationToken);
}