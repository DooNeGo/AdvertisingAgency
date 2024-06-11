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

public sealed partial class ChooseCampaignGoalViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty] private ObservableCollection<CampaignGoal> _campaignGoals = [];

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GoNextCommand))]
    private CampaignGoal? _campaignGoal;

    private Campaign? _campaign;

    public ChooseCampaignGoalViewModel(IMediator mediator) : base(mediator) =>
        UpdateCollectionAsync(CampaignGoals, new GetCampaignGoalsQuery()).SafeFireAndForget();

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