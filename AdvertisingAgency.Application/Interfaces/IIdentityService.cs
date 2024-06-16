using AdvertisingAgency.Application.Dto;

namespace AdvertisingAgency.Application.Interfaces;

public interface IIdentityService
{
    public UserDto? CurrentUser { get; }

    public event Action? LoggedOut;

    public event Action<UserDto>? LoggedIn;

    public Task LoginAsync(string userName, string password, CancellationToken cancellationToken);

    public void Logout();
}