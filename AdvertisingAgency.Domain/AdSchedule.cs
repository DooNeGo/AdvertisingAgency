namespace AdvertisingAgency.Domain;

public readonly record struct AdScheduleId(Guid Value) : IStronglyTypedId<Guid>
{
    public static readonly AdScheduleId Empty = new(Guid.Empty);

    public static AdScheduleId Create() => new(Guid.NewGuid());
}

public sealed class AdSchedule
{
    public AdSchedule(DayOfWeek day, DateTime startTime, DateTime endTime)
    {
        Id = AdScheduleId.Create();
        DayOfWeek = day;
        StartTime = startTime;
        EndTime = endTime;
    }

    private AdSchedule() { }

    public AdScheduleId Id { get; }
    
    public DayOfWeek DayOfWeek { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
}