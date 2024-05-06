namespace AdvertisingAgency.Domain;

public readonly record struct MediaPlanId(Guid Value)
{
    public static readonly MediaPlanId Empty = default;

    public static MediaPlanId CreateNew() => new(Guid.NewGuid());
}

public class MediaPlan
{
    public MediaPlan(DateOnly startDate, DateOnly endDate, List<Service> services)
    {
        Id = MediaPlanId.Empty;
        StartDate = startDate;
        EndDate = endDate;
        Services = services;
    }

    private MediaPlan() { }

    public MediaPlanId Id { get; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public List<Service> Services { get; set; } = [];
}
