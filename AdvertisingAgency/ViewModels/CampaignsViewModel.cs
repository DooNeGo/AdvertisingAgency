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

public sealed partial class CampaignsViewModel : BaseViewModel, IRecipient<AddCampaignMessage>
{
    private readonly IMediator _mediator;
    private readonly IIdentityService _identityService;
    private readonly IPopupService _popupService;

    [ObservableProperty] private ObservableCollection<Campaign> _campaigns = [];
    [ObservableProperty] private bool _isRefreshing;
    [ObservableProperty] private string _currentState = States.Normal;

    public CampaignsViewModel(IMediator mediator, IIdentityService identityService, IPopupService popupService,
        IMessenger messenger) : base(mediator)
    {
        _mediator = mediator;
        _identityService = identityService;
        _popupService = popupService;

        messenger.Register(this);
        IsRefreshing = true;
    }

    public async void Receive(AddCampaignMessage message)
    {
        if (Campaigns.Any(campaign => campaign.Id == message.Value)) return;
        Campaign campaign = await _mediator.Send(new GetCampaignByIdQuery(message.Value)).ConfigureAwait(false);
        Campaigns.Add(campaign);
    }

    [RelayCommand]
    private async Task RefreshAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            ClientId id = _identityService.CurrentUser?.Client.Id ?? throw new NotLoggedInException();
            await UpdateCollectionAsync(Campaigns, new GetCampaignsQuery(id), cancellationToken)
                .ConfigureAwait(false);
        }
        finally
        {
            IsRefreshing = false;
        }
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

        if (castedResult is CampaignActionMenuResult.Delete)
        {
            await DeleteCampaignAsync(campaign, cancellationToken).ConfigureAwait(false);
        }
        else if (castedResult is CampaignActionMenuResult.Edit)
        {
            await Shell.Current.GoToAsync(nameof(ChooseCampaignGoalViewModel),
                new Dictionary<string, object> { { "Campaign", campaign } })
                .WaitAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }

    partial void OnIsRefreshingChanged(bool value)
    {
        if (value) return;
        UpdateCurrentState();
    }

    private void UpdateCurrentState() => CurrentState = Campaigns.Count > 0 ? States.Normal : States.Empty;

    private async Task DeleteCampaignAsync(Campaign campaign, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (await App.Current!.MainPage!.DisplayAlert("Удаление",
                "Вы уверены, что хотите удалить кампанию? Этот процесс будет необратим", "Да", "Нет")
                .WaitAsync(cancellationToken).ConfigureAwait(false))
        {
            Campaigns.Remove(campaign);
        }
    }

    //private Task ShowCampaignDeleteAlert(CancellationToken cancellationToken)
    //{
    //    TaskCompletionSource<bool> result = new();
    //    App.Current!.Dispatcher.DispatchAsync(async () =>
    //    {
    //        Guard.IsNotNull(App.Current!.MainPage, nameof(App.Current.MainPage));
    //        bool answer = await App.Current!.MainPage.DisplayAlert(
    //            "Удаление",
    //            "Вы уверены, что хотите удалить кампанию? Этот процесс будет необратим",
    //            "Да", "Нет").WaitAsync(cancellationToken);
    //        result.SetResult(answer);
    //    }).WaitAsync(cancellationToken).ConfigureAwait(false);
    //    return result.Task.WaitAsync(cancellationToken);
    //}

    private static class States
    {
        public const string Normal = nameof(Normal);
        public const string Empty = nameof(Empty);
    }
}