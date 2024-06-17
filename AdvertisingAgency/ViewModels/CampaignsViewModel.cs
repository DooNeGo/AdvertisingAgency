using AdvertisingAgency.Application.Commands;
using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AdvertisingAgency.Domain.Exceptions;
using AdvertisingAgency.Messages;
using AdvertisingAgency.PopupModels;
using AdvertisingAgency.Popups;
using AdvertisingAgency.ViewModels.CreateCampaign;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mediator;
using System.Collections.ObjectModel;

namespace AdvertisingAgency.ViewModels;

public sealed partial class CampaignsViewModel : BaseViewModel, IRecipient<AddCampaignMessage>, IRecipient<UpdateCampaignMessage>
{
    private readonly IMediator _mediator;
    private readonly IIdentityService _identityService;
    private readonly IPopupService _popupService;
    private readonly IDialogService _dialogService;

    [ObservableProperty] private ObservableCollection<Campaign> _campaigns = [];
    [ObservableProperty] private bool _isRefreshing;
    [ObservableProperty] private string _currentState = States.Normal;

    public CampaignsViewModel(IMediator mediator, IIdentityService identityService, IPopupService popupService,
        IMessenger messenger, IDialogService dialogService) : base(mediator)
    {
        (_mediator, _identityService) = (mediator, identityService);
        (_popupService, _dialogService) = (popupService, dialogService);

        messenger.Register<AddCampaignMessage>(this);
        messenger.Register<UpdateCampaignMessage>(this);
        IsRefreshing = true;
    }

    public void Receive(AddCampaignMessage message)
    {
        if (Campaigns.Any(campaign => campaign.Id == message.Value)) return;
        IsRefreshing = true;
    }

    public void Receive(UpdateCampaignMessage message)
    {
        if (Campaigns.All(campaign => campaign.Id != message.Value)) return;
        IsRefreshing = true;
    }

    [RelayCommand]
    private async Task RefreshAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            ClientId id = _identityService.CurrentUser?.Id ?? throw new NotLoggedInException();
            await UpdateCollectionAsync(Campaigns, new GetCampaignsQuery(id), cancellationToken)
                .ConfigureAwait(false);
        }
        catch
        {
            await _dialogService.ShowInfoAsync("Обновление", "Ошибка", cancellationToken)
                .ConfigureAwait(false);
        }
        finally
        {
            IsRefreshing = false;
        }

        UpdateCurrentState();
    }

    [RelayCommand]
    private Task CreateCampaign(CancellationToken cancellationToken) =>
        Shell.Current.GoToAsync(nameof(ChooseCampaignGoalViewModel))
            .WaitAsync(cancellationToken);

    [RelayCommand]
    private async Task ShowCampaignActionMenuAsync(Campaign campaign, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        object? result = await _popupService
            .ShowPopupAsync<CampaignActionMenuPopupModel>(model => model.Campaign = campaign, cancellationToken);

        if (result is null) return;
        var castedResult = (CampaignActionMenuResult)result;
        await ExecuteCampaignActionAsync(castedResult, campaign, cancellationToken).ConfigureAwait(false);
    }

    private Task ExecuteCampaignActionAsync(CampaignActionMenuResult result, Campaign campaign,
        CancellationToken token = default) => result switch
        {
            CampaignActionMenuResult.Delete => DeleteCampaignAsync(campaign, token),
            CampaignActionMenuResult.Edit => EditCampaignAsync(campaign, token),
            _ => Task.CompletedTask
        };

    private static Task EditCampaignAsync(Campaign campaign, CancellationToken cancellationToken = default) =>
        Shell.Current
            .GoToAsync(nameof(ChooseCampaignGoalViewModel), new Dictionary<string, object> { { "Campaign", campaign } })
            .WaitAsync(cancellationToken);

    private void UpdateCurrentState() => CurrentState = Campaigns.Count > 0 ? States.Normal : States.Empty;

    private async Task DeleteCampaignAsync(Campaign campaign, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        bool answer = await _dialogService.ShowQuestionAsync("Удаление",
            "Вы уверены, что хотите удалить кампанию? Этот процесс будет необратим",
             cancellationToken).ConfigureAwait(false);

        if (!answer) return;

        try
        {
            Campaigns.Remove(campaign);

            await _mediator.Send(new DeleteCampaignByIdCommand(campaign.Id), cancellationToken)
                .ConfigureAwait(false);
        }
        catch
        {
            await _dialogService.ShowInfoAsync("Удаление", "Ошибка", cancellationToken)
                .ConfigureAwait(false);
        }
    }

    private static class States
    {
        public const string Normal = nameof(Normal);
        public const string Empty = nameof(Empty);
    }
}