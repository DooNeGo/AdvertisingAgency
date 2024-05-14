namespace AdvertisingAgency.Domain;

public readonly record struct LocationId(Guid Value)
{
    public static readonly LocationId Empty = new();

    public static LocationId Create() => new(Guid.NewGuid());
}

public sealed class Location
{
    public Location(string title)
    {
        Id = LocationId.Create();
        Title = title;
    }

    private Location() { }

    public LocationId Id { get; }

    public string Title { get; set; } = string.Empty;

    public override string ToString() => Title;
}