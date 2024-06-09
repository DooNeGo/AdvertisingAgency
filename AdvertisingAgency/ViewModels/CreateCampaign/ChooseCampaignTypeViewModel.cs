using System.Collections.ObjectModel;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AsyncAwaitBestPractices;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace AdvertisingAgency.ViewModels.CreateCampaign;

public sealed partial class ChooseCampaignTypeViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty] private ObservableCollection<CampaignType> _campaignTypes = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GoNextCommand))]
    private CampaignType? _campaignType;

    private CampaignGoal? _campaignGoal;

    public ChooseCampaignTypeViewModel(IMediator mediator) : base(mediator) =>
        UpdateCollectionAsync(CampaignTypes, new GetCampaignTypesQuery(), CancellationToken.None).SafeFireAndForget();

    public void ApplyQueryAttributes(IDictionary<string, object> query) =>
        _campaignGoal = (CampaignGoal)query["CampaignGoal"];

    [RelayCommand]
    private void SetCampaignType(CampaignType type) => CampaignType = type;

    private bool CanExecuteGoNext() => CampaignType is not null;

    [RelayCommand(CanExecute = nameof(CanExecuteGoNext))]
    private Task GoNext(CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(_campaignGoal, nameof(_campaignGoal));
        Guard.IsNotNull(CampaignType, nameof(CampaignType));

        return Shell.Current
            .GoToAsync(nameof(CampaignSettingsViewModel), new Dictionary<string, object>
            {
                { "CampaignGoal", _campaignGoal }, { "CampaignType", CampaignType }
            }).WaitAsync(cancellationToken);
    }
}