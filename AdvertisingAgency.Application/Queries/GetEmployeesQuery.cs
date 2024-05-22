using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Queries;

public sealed record GetEmployeesQuery : IQuery<List<Employee>>;

public sealed class GetEmployeesQueryHandler(IApplicationContext context)
    : IQueryHandler<GetEmployeesQuery, List<Employee>>
{
    public ValueTask<List<Employee>> Handle(GetEmployeesQuery query, CancellationToken cancellationToken) =>
        new(context.Employees.ToListAsync(cancellationToken: cancellationToken));
}