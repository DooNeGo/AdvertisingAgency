namespace AdvertisingAgency.Domain;

public readonly record struct UserId(Guid Value)
{
    public static readonly UserId Empty = new();

    public static UserId Create() => new(Guid.NewGuid());
}

public class User
{
    public User(string userName, string password)
    {
        Id = UserId.Empty;
        UserName = userName;
        Password = password;
    }

    private User() { }

    public UserId Id { get; }

    public string UserName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
