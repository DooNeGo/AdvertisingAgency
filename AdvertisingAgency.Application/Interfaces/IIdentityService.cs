using AdvertisingAgency.Domain;

namespace AdvertisingAgency.Application.Interfaces;

public interface IIdentityService
{
    public User? CurrentUser { get; }

    public event Action? LoggedOut;

    public event Action<User>? LoggedIn;

    public Task LoginAsync(string userName, string password, CancellationToken cancellationToken);

    public void Logout();
}