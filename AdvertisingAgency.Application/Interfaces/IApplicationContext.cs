using AdvertisingAgency.Domain;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingAgency.Application.Interfaces;

public interface IApplicationContext : IDisposable, IAsyncDisposable
{
    public DbSet<Campaign> Campaigns { get; }

    public DbSet<CampaignSettings> CampaignSettings { get; }

    public DbSet<Client> Clients { get; }

    public DbSet<Employee> Employees { get; }

    public DbSet<Position> Positions { get; }

    public DbSet<User> Users { get; }

    public DbSet<AdSchedule> AdSchedules { get; }

    public int SaveChanges();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}