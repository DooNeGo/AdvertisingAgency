namespace AdvertisingAgency.Domain;

public readonly record struct AdScheduleId(Guid Value)
{
    public static readonly AdScheduleId Empty = new();

    public static AdScheduleId Create() => new(Guid.NewGuid());
}

public sealed class AdSchedule
{
    public AdSchedule(DayOfWeek startDay, DayOfWeek endDay, TimeOnly startTime, TimeOnly endTime)
    {
        Id = AdScheduleId.Create();
        StartDay = startDay;
        EndDay = endDay;
        StartTime = startTime;
        EndTime = endTime;
    }

    private AdSchedule() { }

    public AdScheduleId Id { get; }
    
    public DayOfWeek StartDay { get; set; }
    
    public DayOfWeek EndDay { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
}