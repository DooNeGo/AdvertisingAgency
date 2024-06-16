using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AdvertisingAgency.Application.Commands;

public sealed record RegisterUserCommand(User User) : ICommand<UserId>;

public sealed class RegisterUserCommandHandler(IApplicationContext context, IHashService hashService)
    : ICommandHandler<RegisterUserCommand, UserId>
{
    public async ValueTask<UserId> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        User user = command.User with { Password = hashService.HashPassword(command.User.Password) };
        EntityEntry<User> entry = await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entry.Entity.Id;
    }
}