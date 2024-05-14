using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Queries;

public sealed class GetLocationsQuery : IQuery<List<Location>>;

public sealed class GetLocationsQueryHandler(IApplicationContext context) : IQueryHandler<GetLocationsQuery, List<Location>>
{
    public ValueTask<List<Location>> Handle(GetLocationsQuery query, CancellationToken cancellationToken) =>
        new(context.Locations.AsNoTracking().ToListAsync(cancellationToken));
}