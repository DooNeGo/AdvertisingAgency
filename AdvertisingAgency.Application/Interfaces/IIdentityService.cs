using AdvertisingAgency.Domain;

namespace AdvertisingAgency.Application.Interfaces;

public interface IIdentityService
{
    public User? CurrentUser { get; }
    
    public event Action<User>? Authorized;
    
    public Task AuthorizeAsync(string userName, string password, CancellationToken cancellationToken);
}