using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Queries;

public sealed record GetEmployeesQuery : IQuery<IAsyncEnumerable<Employee>>;

internal sealed class GetEmployeesQueryHandler(IApplicationContext context)
    : IQueryHandler<GetEmployeesQuery, IAsyncEnumerable<Employee>>
{
    public ValueTask<IAsyncEnumerable<Employee>> Handle(GetEmployeesQuery query, CancellationToken cancellationToken) =>
        new(context.Employees
            .AsNoTracking()
            .Include(employee => employee.Position)
            .Include(employee => employee.FullName)
            .AsAsyncEnumerable());
}