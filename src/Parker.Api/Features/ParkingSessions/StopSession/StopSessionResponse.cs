using Parker.Domain.Features.ParkingSessions.Enums;

namespace Parker.Api.Features.ParkingSessions.StopSession;

public sealed record StopSessionResponse
{
    public required Guid SessionId { get; init; }
    public required Guid UserId { get; init; }
    public required Guid ParkingSpotId { get; init; }
    public required DateTime StartTimeUtc { get; init; }
    public required DateTime EndTimeUtc { get; init; }
    public required ParkingSessionStatus Status { get; init; }
}