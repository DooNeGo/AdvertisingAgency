using AdvertisingAgency.Application.Dto;
using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Infrastructure;

internal sealed class IdentityService(IApplicationContext context, IHashService hashService) : IIdentityService
{
    public UserDto? CurrentUser { get; private set; }

    public event Action? LoggedOut;

    public event Action<UserDto>? LoggedIn;

    public async Task LoginAsync(string userName, string password, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        CurrentUser = await context.Users
            .AsNoTracking()
            .Where(user => user.UserName == userName && user.Password == hashService.HashPassword(password))
            .Include(user => user.Client)
            .ThenInclude(client => client.FullName)
            .Select(user => MapToUserDto(user))
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (CurrentUser is not null) LoggedIn?.Invoke(CurrentUser);
    }

    public void Logout()
    {
        CurrentUser = null;
        LoggedOut?.Invoke();
    }

    private static UserDto MapToUserDto(User user)
    {
        var client = user.Client;
        return new UserDto
        (
            client.Id,
            user.UserName,
            client.PhoneNumber,
            client.CompanyName,
            client.FullName,
            client.Country
        );
    }
}