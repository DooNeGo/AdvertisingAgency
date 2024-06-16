
namespace AdvertisingAgency.Application.Interfaces;

public interface IDialogService
{
    Task ShowInfoAsync(string title, string message, CancellationToken cancellationToken = default);
    Task ShowInfoAsync(string title, string message, string cancel, CancellationToken cancellationToken = default);
    Task<bool> ShowQuestionAsync(string title, string message, string accept, string cancel, CancellationToken cancellationToken = default);
    Task<bool> ShowQuestionAsync(string title, string message, CancellationToken cancellationToken = default);
}
