namespace AdvertisingAgency.Domain;

public readonly record struct LanguageId(Guid Value)
{
    public static readonly LanguageId Empty = default;

    public static LanguageId Create() => new(Guid.NewGuid());
}

public sealed class Language
{
    public Language(string title)
    {
        Id = LanguageId.Create();
        Title = title;
    }

    private Language() { }

    public LanguageId Id { get; }

    public string Title { get; set; } = string.Empty;
    
    public override string ToString() => Title;
}