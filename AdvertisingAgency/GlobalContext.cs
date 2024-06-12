using AdvertisingAgency.Domain;
using System.Collections.Immutable;

namespace AdvertisingAgency;

public interface IGlobalContext
{
    ImmutableArray<CampaignGoal> CampaignGoals { get; }
    ImmutableArray<CampaignType> CampaignTypes { get; }
    ImmutableArray<Country> Countries { get; }
    ImmutableArray<Language> Languages { get; }
    ImmutableArray<DayOfWeek> DayOfWeeks { get; }
}

public sealed class GlobalContext : IGlobalContext
{
    public ImmutableArray<Country> Countries { get; } = [.. Enum.GetValues<Country>()];

    public ImmutableArray<Language> Languages { get; } = [.. Enum.GetValues<Language>()];

    public ImmutableArray<CampaignGoal> CampaignGoals { get; } = [.. Enum.GetValues<CampaignGoal>()];

    public ImmutableArray<CampaignType> CampaignTypes { get; } = [.. Enum.GetValues<CampaignType>()];

    public ImmutableArray<DayOfWeek> DayOfWeeks { get; } = [.. Enum.GetValues<DayOfWeek>()];
}
