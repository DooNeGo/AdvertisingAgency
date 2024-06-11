namespace AdvertisingAgency.Domain;

public readonly record struct CampaignId(Guid Value) : IStronglyTypedId<Guid>
{
    public static readonly CampaignId Empty = new(Guid.Empty);

    public static CampaignId Create() => new(Guid.NewGuid());
}

public sealed class Campaign
{
    public Campaign(ClientId clientId,
        EmployeeId employeeId,
        CampaignGoalId goalId,
        CampaignTypeId typeId,
        CampaignSettings settings,
        string name)
    {
        Id = CampaignId.Create();
        Name = name;
        Status = CampaignStatus.Reviewing;
        ClientId = clientId;
        EmployeeId = employeeId;
        GoalId = goalId;
        TypeId = typeId;
        Settings = settings;
    }

    private Campaign() { }

    public CampaignId Id { get; }

    public string Name { get; set; } = string.Empty;

    public CampaignStatus Status { get; set; }

    public Client Client { get; set; } = null!;

    public Employee Employee { get; set; } = null!;

    public CampaignGoal Goal { get; set; } = null!;

    public CampaignSettings Settings { get; set; } = null!;

    public CampaignType Type { get; set; } = null!;

    public ClientId ClientId { get; }

    public EmployeeId EmployeeId { get; set; }

    public CampaignGoalId GoalId { get; set; }

    public CampaignSettingsId SettingsId { get; }

    public CampaignTypeId TypeId { get; set; }
}