using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace AdvertisingAgency.Infrastructure;

internal sealed class ApplicationContext : DbContext, IApplicationContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Batteries_V2.Init();
        //Database.EnsureDeleted();
        Database.EnsureCreated();
        this.LoadData();
    }

    public DbSet<Campaign> Campaigns => Set<Campaign>();

    public DbSet<CampaignSettings> CampaignSettings => Set<CampaignSettings>();

    public DbSet<Client> Clients => Set<Client>();

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Position> Positions => Set<Position>();

    public DbSet<User> Users => Set<User>();

    public DbSet<AdSchedule> AdSchedules => Set<AdSchedule>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        path = Path.Combine(path, "AdvertisingAgencyDB.db3");

        optionsBuilder.UseSqlite($"Data Source={path}")
            .EnableSensitiveDataLogging()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyStronglyTypedIdConversion<AdScheduleId, Guid>()
            .ApplyStronglyTypedIdConversion<CampaignId, Guid>()
            .ApplyStronglyTypedIdConversion<CampaignSettingsId, Guid>()
            .ApplyStronglyTypedIdConversion<ClientId, Guid>()
            .ApplyStronglyTypedIdConversion<EmployeeId, Guid>()
            .ApplyStronglyTypedIdConversion<FullNameId, Guid>()
            .ApplyStronglyTypedIdConversion<PositionId, Guid>()
            .ApplyStronglyTypedIdConversion<UserId, Guid>();

        modelBuilder.Entity<Campaign>().HasKey(campaign => campaign.Id);
        modelBuilder.Entity<CampaignSettings>().HasKey(settings => settings.Id);
        modelBuilder.Entity<Client>().HasKey(client => client.Id);
        modelBuilder.Entity<Employee>().HasKey(employee => employee.Id);
        modelBuilder.Entity<Position>().HasKey(position => position.Id);
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<AdSchedule>().HasKey(schedule => schedule.Id);
        modelBuilder.Entity<FullName>().HasKey(name => name.Id);

        modelBuilder.Entity<User>().HasIndex(user => user.UserName).IsUnique();
        modelBuilder.Entity<Client>().HasIndex(client => client.PhoneNumber).IsUnique();
    }
}