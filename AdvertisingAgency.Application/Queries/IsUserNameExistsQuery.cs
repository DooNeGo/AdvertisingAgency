using AdvertisingAgency.Application.Interfaces;
using Mediator;

namespace AdvertisingAgency.Application.Queries;

public sealed record IsUserNameExistsQuery(string UserName) : IQuery<bool>;

internal sealed class IsUserNameExistsQueryHandler(IApplicationContext context)
    : IQueryHandler<IsUserNameExistsQuery, bool>
{
    public ValueTask<bool> Handle(IsUserNameExistsQuery query, CancellationToken cancellationToken) =>
        new(context.Users.Any(user => user.UserName == query.UserName));
}