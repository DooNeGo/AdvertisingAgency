using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace AdvertisingAgency.ViewModels.CreateCampaign;

public sealed partial class ChooseCampaignGoalViewModel : ObservableObject
{
    [ObservableProperty] private List<CampaignGoal> _campaignGoals = [];
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GoNextCommand))]
    private CampaignGoal? _campaignGoal;

    public ChooseCampaignGoalViewModel(IMediator mediator) =>
        Task.Run(async () => CampaignGoals = await mediator.Send(new GetCampaignGoalsQuery()));

    [RelayCommand] 
    private void SetCampaignGoal(CampaignGoal goal) => CampaignGoal = goal;

    [RelayCommand(CanExecute = nameof(CanExecuteGoNext))]
    private Task GoNext() =>
        Shell.Current.GoToAsync(nameof(ChooseCampaignTypeViewModel),
            new Dictionary<string, object> { { "CampaignGoal", CampaignGoal! } });

    private bool CanExecuteGoNext() => CampaignGoal is not null;
}