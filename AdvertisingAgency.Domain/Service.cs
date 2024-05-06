namespace AdvertisingAgency.Domain;

public readonly record struct ServiceId(Guid Value)
{
    public static readonly ServiceId Empty = default;

    public static ServiceId CreateNew() => new(Guid.NewGuid());
}

public class Service
{
    public Service(string title, decimal price)
    {
        Id = ServiceId.Empty;
        Title = title;
        Price = price;
    }

    private Service() { }

    public ServiceId Id { get; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }
}
