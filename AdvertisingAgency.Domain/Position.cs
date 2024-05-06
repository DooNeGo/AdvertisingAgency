namespace AdvertisingAgency.Domain;

public readonly record struct PositionId(Guid Value)
{
    public static readonly PositionId Empty = default;

    public static PositionId CreateNew() => new(Guid.NewGuid());
}

public class Position
{
    public Position(string title)
    {
        Id = PositionId.Empty;
        Title = title;
    }

    private Position() { }

    public PositionId Id { get; }

    public string Title { get; set; } = string.Empty;
}
