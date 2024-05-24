namespace AdvertisingAgency.Domain;

public readonly record struct AdScheduleId(Guid Value)
{
    public static readonly AdScheduleId Empty;

    public static AdScheduleId Create() => new(Guid.NewGuid());
}

public sealed class AdSchedule
{
    public AdSchedule(DayOfWeek day, TimeSpan startTime, TimeSpan endTime)
    {
        Id = AdScheduleId.Create();
        DayOfWeek = day;
        StartTime = startTime;
        EndTime = endTime;
    }

    private AdSchedule() { }

    public AdScheduleId Id { get; }
    
    public DayOfWeek DayOfWeek { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
}