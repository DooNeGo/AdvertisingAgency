using AdvertisingAgency.Application.Interfaces;

namespace AdvertisingAgency.Services;

public sealed class DialogService : IDialogService
{
    private const string Ok = "Ок";
    private const string Cancel = "Отмена";

    public Task ShowInfoAsync(string title, string message, CancellationToken cancellationToken = default) =>
        ShowInfoAsync(title, message, Ok, cancellationToken);

    public Task<bool> ShowQuestionAsync(string title, string message, CancellationToken cancellationToken = default) =>
        ShowQuestionAsync(title, message, Ok, Cancel, cancellationToken);

    public Task<bool> ShowQuestionAsync(string title, string message, string accept, string cancel,
        CancellationToken cancellationToken = default) =>
        Microsoft.Maui.Controls.Application.Current!.Dispatcher.DispatchAsync(() =>
            Microsoft.Maui.Controls.Application.Current.MainPage!
                .DisplayAlert(title, message, accept, cancel)
                .WaitAsync(cancellationToken));

    public Task ShowInfoAsync(string title, string message, string cancel, CancellationToken cancellationToken = default) =>
        Microsoft.Maui.Controls.Application.Current!.Dispatcher.DispatchAsync(() =>
            Microsoft.Maui.Controls.Application.Current.MainPage!
                .DisplayAlert(title, message, cancel)
                .WaitAsync(cancellationToken));
}