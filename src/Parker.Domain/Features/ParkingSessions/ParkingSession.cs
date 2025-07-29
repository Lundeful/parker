using Parker.Domain.Common;
using Parker.Domain.Features.ParkingSessions.Enums;

namespace Parker.Domain.Features.ParkingSessions;

public sealed class ParkingSession : AggregateRoot, IEntity
{
    public Guid Id { get; private init; }
    public Guid UserId { get; private init; }
    public Guid ParkingSpotId { get; private init; }
    public DateTime StartTimeUtc { get; private init; }
    public DateTime? EndTimeUtc { get; private set; }
    public ParkingSessionStatus Status { get; private set; }

#pragma warning disable CS8618
    private ParkingSession() // Parameterless constructor is needed for EF Core to instantiate this model
    {
    }
#pragma warning restore CS8618

    public static ParkingSession Start(Guid userId, Guid parkingSpotId)
    {
        return new ParkingSession
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            ParkingSpotId = parkingSpotId,
            StartTimeUtc = DateTime.UtcNow,
            EndTimeUtc = null,
            Status = ParkingSessionStatus.Active
        };
    }

    public DateTime Stop()
    {
        if (Status is not ParkingSessionStatus.Active)
        {
            // Here you could opt for custom domain exceptions or return a result type. For simplicity, we throw a more generic exception
            throw new InvalidOperationException("Cannot end a parking session that is not active");
        }

        var endTime = DateTime.UtcNow;

        EndTimeUtc = endTime;
        Status = ParkingSessionStatus.Completed;

        // Here you could add a domain event to this aggregate

        return endTime;
    }
}