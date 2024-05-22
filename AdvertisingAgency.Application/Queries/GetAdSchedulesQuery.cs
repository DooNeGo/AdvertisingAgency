using AdvertisingAgency.Domain;
using Mediator;

namespace AdvertisingAgency.Application.Queries;

public sealed record GetAdSchedulesQuery : IQuery<List<AdSchedule>>;