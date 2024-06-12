using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Infrastructure;

internal sealed class IdentityService(IApplicationContext context) : IIdentityService
{
    public User? CurrentUser { get; private set; }

    public event Action? LoggedOut;
    
    public event Action<User>? LoggedIn;

    public async Task LoginAsync(string userName, string password, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        CurrentUser = await context.Users
            .AsNoTracking()
            .Where(user => user.UserName == userName && user.Password == password)
            .Include(user => user.Client)
            .ThenInclude(client => client.FullName)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (CurrentUser is not null) LoggedIn?.Invoke(CurrentUser);
    }

    public void Logout()
    {
        CurrentUser = null;
        LoggedOut?.Invoke();
    }
}