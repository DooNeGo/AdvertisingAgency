using AdvertisingAgency.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection;

namespace AdvertisingAgency.Infrastructure;

internal static class ModelBuilderExtensions
{
    public static ModelBuilder ApplyStronglyTypedIdConversion<T, R>(this ModelBuilder modelBuilder)
        where T : IStronglyTypedId<R>, new()
    {
        var converter = new ValueConverter<T, R>(id => id.Value, guid => new T() { Value = guid });

        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            IEnumerable<PropertyInfo> properties = entityType.ClrType
                .GetProperties()
                .Where(p => p.PropertyType == typeof(T));

            foreach (PropertyInfo property in properties)
            {
                modelBuilder.Entity(entityType.Name)
                    .Property(property.Name)
                    .HasConversion(converter);
            }
        }

        return modelBuilder;
    }
}
