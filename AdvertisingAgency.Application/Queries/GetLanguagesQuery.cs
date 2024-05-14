using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Queries;

public sealed class GetLanguagesQuery : IQuery<List<Language>>;

public sealed class GetLanguagesQueryHandler(IApplicationContext context) : IQueryHandler<GetLanguagesQuery, List<Language>>
{
    public ValueTask<List<Language>> Handle(GetLanguagesQuery query, CancellationToken cancellationToken) =>
        new(context.Languages.AsNoTracking().ToListAsync(cancellationToken));
}