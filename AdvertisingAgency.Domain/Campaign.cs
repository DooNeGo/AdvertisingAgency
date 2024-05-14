namespace AdvertisingAgency.Domain;

public readonly record struct CampaignId(Guid Value)
{
    public static readonly CampaignId Empty = new();

    public static CampaignId Create() => new(Guid.NewGuid());
}

public sealed class Campaign
{
    public Campaign(Client client,
        Employee employee,
        CampaignGoal goal,
        CampaignType type,
        CampaignSettings settings,
        string name,
        decimal budget)
    {
        Id = CampaignId.Create();
        Status = CampaignStatus.Reviewing;
        Client = client;
        Employee = employee;
        Goal = goal;
        Type = type;
        Settings = settings;
        Name = name;
        Budget = budget;
    }

    private Campaign() { }

    public CampaignId Id { get; }
    
    public CampaignStatus Status { get; set; }

    public Client Client { get; set; } = null!;

    public Employee Employee { get; set; } = null!;
    
    public CampaignGoal Goal { get; set; } = null!;

    public CampaignSettings Settings { get; set; } = null!;

    public CampaignType Type { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public decimal Budget { get; set; }
}