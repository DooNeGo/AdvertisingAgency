using System.Collections.ObjectModel;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AsyncAwaitBestPractices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace AdvertisingAgency.ViewModels.CreateCampaign;

public sealed partial class ChooseCampaignGoalViewModel : BaseViewModel
{
    [ObservableProperty] private ObservableCollection<CampaignGoal> _campaignGoals = [];
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GoNextCommand))]
    private CampaignGoal? _campaignGoal;

    public ChooseCampaignGoalViewModel(IMediator mediator) : base(mediator) =>
        UpdateCollectionAsync(CampaignGoals, new GetCampaignGoalsQuery(), CancellationToken.None).SafeFireAndForget();

    [RelayCommand] 
    private void SetCampaignGoal(CampaignGoal goal) => CampaignGoal = goal;

    [RelayCommand(CanExecute = nameof(CanExecuteGoNext))]
    private Task GoNext() =>
        Shell.Current.GoToAsync(nameof(ChooseCampaignTypeViewModel),
            new Dictionary<string, object> { { "CampaignGoal", CampaignGoal! } });

    private bool CanExecuteGoNext() => CampaignGoal is not null;
}