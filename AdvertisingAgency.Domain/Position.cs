namespace AdvertisingAgency.Domain;

public readonly record struct PositionId(Guid Value)
{
    public static readonly PositionId Empty;

    public static PositionId Create() => new(Guid.NewGuid());
}

public sealed class Position
{
    public Position(string title)
    {
        Id = PositionId.Create();
        Title = title;
    }

    private Position() { }

    public PositionId Id { get; }

    public string Title { get; set; } = string.Empty;

    public override string ToString() => Title;
}
