using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AsyncAwaitBestPractices;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AdvertisingAgency.ViewModels.CreateCampaign;

public sealed partial class ChooseCampaignTypeViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty] private ObservableCollection<CampaignType> _campaignTypes = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GoNextCommand))]
    private CampaignType? _campaignType;

    private CampaignGoal? _campaignGoal;
    private Campaign? _campaign;

    public ChooseCampaignTypeViewModel(IMediator mediator) : base(mediator) =>
        UpdateCollectionAsync(CampaignTypes, new GetCampaignTypesQuery()).SafeFireAndForget();

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Campaign", out object? campaign))
        {
            _campaign = (Campaign)campaign;
            PropertyChanged += OnCampaignTypeChanged;
            SetCampaignType(_campaign.Type);
        }
        else
        {
            _campaignGoal = (CampaignGoal)query["CampaignGoal"];
        }
    }

    private void OnCampaignTypeChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is not nameof(CampaignType)) return;
        Guard.IsNotNull(CampaignType, nameof(CampaignType));
        _campaign!.Type = CampaignType;
    }

    [RelayCommand]
    private void SetCampaignType(CampaignType type) => CampaignType = type;

    [RelayCommand(CanExecute = nameof(CanExecuteGoNext))]
    private Task GoNextAsync(CancellationToken cancellationToken = default)
    {
        Dictionary<string, object?> parameters = [];

        if (_campaign is null)
        {
            Guard.IsNotNull(_campaignGoal, nameof(_campaignGoal));
            Guard.IsNotNull(CampaignType, nameof(CampaignType));

            parameters.Add("CampaignGoal", _campaignGoal);
            parameters.Add("CampaignType", CampaignType);
        }
        else
        {
            Guard.IsNotNull(_campaign, nameof(_campaign));
            parameters.Add("Campaign", _campaign);
        }

        return Shell.Current
            .GoToAsync(nameof(CampaignSettingsViewModel), parameters)
            .WaitAsync(cancellationToken);
    }

    private bool CanExecuteGoNext() => CampaignType is not null;
}