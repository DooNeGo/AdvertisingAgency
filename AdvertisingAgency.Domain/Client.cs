namespace AdvertisingAgency.Domain;

public readonly record struct ClientId(Guid Value) : IStronglyTypedId<Guid>
{
    public static readonly ClientId Empty = new(Guid.Empty);

    public static ClientId Create() => new(Guid.NewGuid());
}

public sealed class Client
{
    public Client(string companyName, string phoneNumber, FullName fullName, Country country)
    {
        Id = ClientId.Create();
        CompanyName = companyName;
        Country = country;
        PhoneNumber = phoneNumber;
        FullName = fullName;
        Campaigns = [];
    }

    private Client() { }

    public ClientId Id { get; }

    public string CompanyName { get; set; } = string.Empty;

    public Country Country { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public FullName FullName { get; set; } = null!;

    public List<Campaign> Campaigns { get; } = null!;
}
