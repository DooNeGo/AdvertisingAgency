using System.Collections.ObjectModel;
using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Application.Queries;
using AdvertisingAgency.Domain;
using AdvertisingAgency.Messages;
using AdvertisingAgency.PopupModels;
using AdvertisingAgency.Popups;
using AdvertisingAgency.ViewModels.CreateCampaign;
using AsyncAwaitBestPractices;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mediator;

namespace AdvertisingAgency.ViewModels;

public sealed partial class CampaignsViewModel : BaseViewModel, IRecipient<AddCampaignMessage>
{
    private readonly IIdentityService _identityService;
    private readonly IPopupService _popupService;

    [ObservableProperty] private ObservableCollection<Campaign> _campaigns = [];
    [ObservableProperty] private bool _isRefreshing;

    public CampaignsViewModel(IMediator mediator, IIdentityService identityService, IPopupService popupService,
        IMessenger messenger) : base(mediator)
    {
        _identityService = identityService;
        _popupService = popupService;
        messenger.Register(this);
        RefreshAsync(CancellationToken.None).SafeFireAndForget();
    }
    
    public void Receive(AddCampaignMessage message)
    {
        throw new NotImplementedException();
    }

    [RelayCommand]
    private async Task RefreshAsync(CancellationToken cancellationToken)
    {
        ClientId id = _identityService.CurrentUser!.Client.Id;
        await UpdateCollectionAsync(Campaigns, new GetCampaignsQuery(id), cancellationToken).ConfigureAwait(false);
        IsRefreshing = false;
    }

    [RelayCommand]
    private Task CreateCampaign(CancellationToken cancellationToken) => 
        Shell.Current.GoToAsync(nameof(ChooseCampaignGoalViewModel))
            .WaitAsync(cancellationToken);

    [RelayCommand]
    private async Task ShowCampaignActionMenuAsync(Campaign campaign, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        object? result = await _popupService.ShowPopupAsync<CampaignActionMenuPopupModel>(
            model => model.Campaign = campaign, cancellationToken);

        if (result is null) return;
        
        var castedResult = (CampaignActionMenuResult)result;
        
        if (castedResult is CampaignActionMenuResult.Delete)
        {
            await DeleteCampaignAsync(campaign, cancellationToken).ConfigureAwait(false);
        }
    }

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
}