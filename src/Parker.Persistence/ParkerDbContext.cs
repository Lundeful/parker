using Microsoft.EntityFrameworkCore;
using Parker.Domain.Features.ParkingSessions;
using Parker.Domain.Features.ParkingSpots;

namespace Parker.Persistence;

public sealed class ParkerDbContext : DbContext
{
    public ParkerDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ParkingSpot> ParkingSpots { get; init; }
    public DbSet<ParkingSession> ParkingSessions { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Applies all IEntityTypeConfiguration<T>
        builder.ApplyConfigurationsFromAssembly(typeof(ParkerDbContext).Assembly);

        // Add value object conversions if necessary
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        // Here you could add a call to dispatch/process domain events
        return await base.SaveChangesAsync(cancellationToken);
    }
}