using AdvertisingAgency.Domain;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;
using System.Collections.Immutable;

namespace AdvertisingAgency.ViewModels.CreateCampaign;

public sealed partial class ChooseCampaignTypeViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty] private ImmutableArray<CampaignType> _campaignTypes = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GoNextCommand))]
    private CampaignType? _campaignType;

    private CampaignGoal? _campaignGoal;
    private Campaign? _campaign;

    public ChooseCampaignTypeViewModel(IMediator mediator, IGlobalContext globalContext) : base(mediator) =>
        CampaignTypes = globalContext.CampaignTypes;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Campaign", out object? campaign))
        {
            _campaign = (Campaign)campaign;
            SetCampaignType(_campaign.Type);
        }
        _campaignGoal = (CampaignGoal)query["CampaignGoal"];
    }

    [RelayCommand]
    private void SetCampaignType(CampaignType type) => CampaignType = type;

    [RelayCommand(CanExecute = nameof(CanExecuteGoNext))]
    private Task GoNextAsync(CancellationToken cancellationToken = default) =>
        Shell.Current.GoToAsync(nameof(CampaignSettingsViewModel), GetNavigationParameters())
            .WaitAsync(cancellationToken);

    private Dictionary<string, object?> GetNavigationParameters()
    {
        Guard.IsNotNull(_campaignGoal, nameof(_campaignGoal));
        Guard.IsNotNull(CampaignType, nameof(CampaignType));

        Dictionary<string, object?> parameters = new()
        {
            { "CampaignGoal", _campaignGoal },
            { "CampaignType", CampaignType }
        };

        if (_campaign is not null)
        {
            parameters.Add("Campaign", _campaign);
        }

        return parameters;
    }

    private bool CanExecuteGoNext() => CampaignType is not null;
}