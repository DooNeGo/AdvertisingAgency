namespace AdvertisingAgency.Domain;

public interface IStronglyTypedId<T>
{
    T Value { get; init; }
}
