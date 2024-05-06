namespace AdvertisingAgency.Domain;

public readonly record struct ClientId(Guid Value)
{
    public static readonly ClientId Empty = default;

    public static ClientId CreateNew() => new(Guid.NewGuid());
}

public class Client
{
    public Client(string companyName, string country, string city, string phoneNumber, string owner)
    {
        Id = ClientId.Empty;
        CompanyName = companyName;
        Country = country;
        City = city;
        PhoneNumber = phoneNumber;
        Owner = owner;
    }

    private Client() { }

    public ClientId Id { get; }

    public string CompanyName { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Owner { get; set; } = string.Empty;
}
