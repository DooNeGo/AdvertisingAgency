using AdvertisingAgency.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Interfaces;

public interface IApplicationContext
{
    public DbSet<Campaign> Campaigns { get; }

    public DbSet<CampaignGoal> CampaignGoals { get; }

    public DbSet<CampaignType> CampaignTypes { get; }

    public DbSet<CampaignSettings> CampaignSettings { get; }
    
    public DbSet<Client> Clients { get; }
    
    public DbSet<Employee> Employees { get; }
    
    public DbSet<Position> Positions { get; }
    
    public DbSet<User> Users { get; }
    
    public DbSet<Location> Locations { get; }
    
    public DbSet<Language> Languages { get; }

    public DbSet<AdSchedule> AdSchedules { get; }

    public int SaveChanges();
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}