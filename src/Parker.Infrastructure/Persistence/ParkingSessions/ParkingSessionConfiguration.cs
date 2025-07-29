using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Parker.Domain.Features.ParkingSessions;

namespace Parker.Infrastructure.Persistence.ParkingSessions;

public sealed class ParkingSessionConfiguration : IEntityTypeConfiguration<ParkingSession>
{
    public void Configure(EntityTypeBuilder<ParkingSession> builder)
    {
        // Configure properties, both existing and shadow properties
        // Add conversions
        // Add indexes
    }
}