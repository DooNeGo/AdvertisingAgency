using AdvertisingAgency.Application.Interfaces;
using Mediator;

namespace AdvertisingAgency.Application.Queries;

public sealed record IsPhoneNumberExistsQuery(string PhoneNumber) : IQuery<bool>;

internal sealed class IsPhoneNumberExistsQueryHandler(IApplicationContext context)
    : IQueryHandler<IsPhoneNumberExistsQuery, bool>
{
    public ValueTask<bool> Handle(IsPhoneNumberExistsQuery query, CancellationToken cancellationToken) =>
        new(context.Clients.Any(client => client.PhoneNumber == query.PhoneNumber));
}
