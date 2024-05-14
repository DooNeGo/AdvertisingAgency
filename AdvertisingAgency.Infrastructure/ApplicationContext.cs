using AdvertisingAgency.Application.Interfaces;
using AdvertisingAgency.Domain;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using Client = AdvertisingAgency.Domain.Client;

namespace AdvertisingAgency.Infrastructure;

internal sealed class ApplicationContext : DbContext, IApplicationContext
{
    public ApplicationContext(DbContextOptions options) : base(options)
    {
        Batteries_V2.Init();
        Database.EnsureDeleted();
        Database.EnsureCreated();
        this.LoadData();
    }

    public DbSet<Campaign> Campaigns => Set<Campaign>();

    public DbSet<CampaignGoal> CampaignGoals => Set<CampaignGoal>();

    public DbSet<CampaignType> CampaignTypes => Set<CampaignType>();

    public DbSet<CampaignSettings> CampaignSettings => Set<CampaignSettings>();
    
    public DbSet<Client> Clients => Set<Client>();
    
    public DbSet<Employee> Employees => Set<Employee>();
    
    public DbSet<Position> Positions => Set<Position>();
    
    public DbSet<User> Users => Set<User>();
    
    public DbSet<Location> Locations => Set<Location>();
    
    public DbSet<Language> Languages => Set<Language>();

    public DbSet<AdSchedule> AdSchedules => Set<AdSchedule>();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        path = Path.Combine(path, "AdvertisingAgencyDB.db3");
        optionsBuilder.UseSqlite($"Data Source={path}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campaign>().HasKey(brief => brief.Id);
        modelBuilder.Entity<Campaign>()
            .Property(brief => brief.Id)
            .HasConversion(id => id.Value, value => new CampaignId(value));
        
        modelBuilder.Entity<CampaignType>().HasKey(type => type.Id);
        modelBuilder.Entity<CampaignType>()
            .Property(type => type.Id)
            .HasConversion(id => id.Value, value => new CampaignTypeId(value));
        
        modelBuilder.Entity<CampaignGoal>().HasKey(goal => goal.Id);
        modelBuilder.Entity<CampaignGoal>()
            .Property(goal => goal.Id)
            .HasConversion(id => id.Value, value => new CampaignGoalId(value));
        
        modelBuilder.Entity<CampaignSettings>().HasKey(settings => settings.Id);
        modelBuilder.Entity<CampaignSettings>()
            .Property(settings => settings.Id)
            .HasConversion(id => id.Value, value => new CampaignSettingsId(value));
        
        modelBuilder.Entity<Client>().HasKey(client => client.Id);
        modelBuilder.Entity<Client>()
            .Property(client => client.Id)
            .HasConversion(id => id.Value, value => new ClientId(value));
        
        modelBuilder.Entity<Employee>().HasKey(employee => employee.Id);
        modelBuilder.Entity<Employee>()
            .Property(employee => employee.Id)
            .HasConversion(id => id.Value, value => new EmployeeId(value));
        
        modelBuilder.Entity<Position>().HasKey(position => position.Id);
        modelBuilder.Entity<Position>()
            .Property(position => position.Id)
            .HasConversion(id => id.Value, value => new PositionId(value));

        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<User>()
            .Property(user => user.Id)
            .HasConversion(id => id.Value, value => new UserId(value));
        
        modelBuilder.Entity<Language>().HasKey(language => language.Id);
        modelBuilder.Entity<Language>()
            .Property(language => language.Id)
            .HasConversion(id => id.Value, value => new LanguageId(value));
        
        modelBuilder.Entity<Location>().HasKey(location => location.Id);
        modelBuilder.Entity<Location>()
            .Property(location => location.Id)
            .HasConversion(id => id.Value, value => new LocationId(value));

        modelBuilder.Entity<AdSchedule>().HasKey(schedule => schedule.Id);
        modelBuilder.Entity<AdSchedule>()
            .Property(schedule => schedule.Id)
            .HasConversion(id => id.Value, value => new AdScheduleId(value));

        modelBuilder.Entity<FullName>().HasKey(name => name.Id);
        modelBuilder.Entity<FullName>()
            .Property(name => name.Id)
            .HasConversion(id => id.Value, value => new FullNameId(value));

    }
}