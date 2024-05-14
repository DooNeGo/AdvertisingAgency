using Microsoft.Extensions.DependencyInjection;

namespace AdvertisingAgency.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection collection) =>
        collection.AddMediator();
}