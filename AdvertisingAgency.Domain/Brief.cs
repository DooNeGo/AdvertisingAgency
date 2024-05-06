namespace AdvertisingAgency.Domain;

public readonly record struct BriefId(Guid Value)
{
    public static readonly BriefId Empty = new();

    public static BriefId Create() => new(Guid.NewGuid());
}

public class Brief
{
    public Brief(Client client, Employee employee, MediaPlan mediaPlan, string mainProduct, string audience, decimal budget, string terms, string goal)
    {
        Client = client;
        Employee = employee;
        MediaPlan = mediaPlan;
        MainProduct = mainProduct;
        Audience = audience;
        Budget = budget;
        Terms = terms;
        Goal = goal;
    }

    private Brief() { }

    public BriefId Id { get; }

    public Client Client { get; set; } = null!;

    public Employee Employee { get; set; } = null!;

    public MediaPlan MediaPlan { get; set; } = null!;

    public string MainProduct { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public decimal Budget { get; set; }

    public string Terms { get; set; } = string.Empty;

    public string Goal { get; set; } = string.Empty;
}
