using Parker.Domain.Features.ParkingSessions.Enums;

namespace Parker.Api.Features.ParkingSessions.StartSession;

public sealed record StartSessionResponse
{
    public required Guid SessionId { get; init; }
    public required Guid UserId { get; init; }
    public required Guid ParkingSpotId { get; init; }
    public required string ParkingSpotName { get; init; }
    public required DateTime StartTimeUtc { get; init; }
    public required ParkingSessionStatus Status { get; init; }
}