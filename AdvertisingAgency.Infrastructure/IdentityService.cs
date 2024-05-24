using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Infrastructure;

internal sealed class IdentityService(IApplicationContext context) : IIdentityService
{
    public User? CurrentUser { get; private set; }

    public event Action<User>? Authorized;

    public async Task AuthorizeAsync(string userName, string password, CancellationToken cancellationToken)
    {
        CurrentUser = await context.Users
            .Where(user => user.UserName == userName && user.Password == password)
            .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

        if (CurrentUser is not null) Authorized?.Invoke(CurrentUser);
    }
}