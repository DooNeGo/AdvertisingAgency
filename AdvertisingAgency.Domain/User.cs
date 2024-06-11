namespace AdvertisingAgency.Domain;

public readonly record struct UserId(Guid Value) : IStronglyTypedId<Guid>
{
    public static readonly UserId Empty = new(Guid.Empty);

    public static UserId Create() => new(Guid.NewGuid());
}

public sealed class User
{
    public User(string userName, string password, Client client)
    {
        Id = UserId.Create();
        UserName = userName;
        Password = password;
        Client = client;
    }

    private User() { }

    public UserId Id { get; }

    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public Client Client { get; set; } = null!;
}
