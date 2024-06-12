using AdvertisingAgency.Domain;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;
using System.Collections.Immutable;

namespace AdvertisingAgency.ViewModels.CreateCampaign;

public sealed partial class ChooseCampaignGoalViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty] private ImmutableArray<CampaignGoal> _campaignGoals = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GoNextCommand))]
    private CampaignGoal? _campaignGoal;

    private Campaign? _campaign;

    public ChooseCampaignGoalViewModel(IMediator mediator, IGlobalContext globalContext) : base(mediator)
    {
        CampaignGoals = globalContext.CampaignGoals;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Campaign", out object? campaign))
        {
            _campaign = (Campaign)campaign;
            SetCampaignGoal(_campaign.Goal);
        }
    }

    [RelayCommand]
    private void SetCampaignGoal(CampaignGoal goal) => CampaignGoal = goal;

    [RelayCommand(CanExecute = nameof(CanExecuteGoNext))]
    private Task GoNextAsync(CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(CampaignGoal, nameof(CampaignGoal));

        Dictionary<string, object> parameters = [];
        parameters.Add("CampaignGoal", CampaignGoal);
        if (_campaign is not null) parameters.Add("Campaign", _campaign);

        return Shell.Current
            .GoToAsync(nameof(ChooseCampaignTypeViewModel), parameters)
            .WaitAsync(cancellationToken);
    }

    private bool CanExecuteGoNext() => CampaignGoal is not null;
}