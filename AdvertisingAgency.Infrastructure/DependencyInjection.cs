using AdvertisingAgency.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertisingAgency.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection collection) =>
        collection.AddDbContext<IApplicationContext, ApplicationContext>()
            .AddSingleton<IIdentityService, IdentityService>()
            .AddSingleton<IHashService, HashService>();
}