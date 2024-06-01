namespace AdvertisingAgency.Domain;

public readonly record struct CampaignGoalId(Guid Value)
{
    public static readonly CampaignGoalId Empty = new(Guid.Empty);

    public static CampaignGoalId Create() => new(Guid.NewGuid());
}

public sealed class CampaignGoal
{
    public CampaignGoal(string title, string description)
    {
        Id = CampaignGoalId.Create();
        Title = title;
        Description = description;
    }
    
    private CampaignGoal () { }

    public CampaignGoalId Id { get; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
}