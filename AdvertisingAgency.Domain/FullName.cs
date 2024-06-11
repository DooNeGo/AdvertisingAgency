namespace AdvertisingAgency.Domain;

public readonly record struct FullNameId(Guid Value) : IStronglyTypedId<Guid>
{
    public static readonly FullNameId Empty = new(Guid.Empty);

    public static FullNameId Create() => new(Guid.NewGuid());
}

public sealed record FullName(string FirstName, string LastName)
{
    public FullNameId Id { get; } = FullNameId.Create();
    
    public override string ToString() => $"{FirstName} {LastName}";
}