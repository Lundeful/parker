using Parker.Domain.Features.ParkingSessions.Enums;

namespace Parker.Api.Features.ParkingSessions.GetRecentlyCompletedSessions;

// This would probably be a paginated response in a real world scenario
public sealed record GetRecentlyCompletedSessionsResponse
{
    public required IReadOnlyCollection<GetRecentlyCompletedSessionsParkingSession> Sessions { get; init; }
}

// Since different endpoints might return different versions of a "ParkingSession" depending on where it is used, with differing details,
// I prefix it with the feature name to separate them. This could be done with namespaces, so this would stay as "ParkingSession", but full
// names make it work well for the frontend that generate types based on OpenApi docs. Other naming schemes may work just as well, as long
// as they are unique. Just because some responses for retrieving a ParkingSession may look *similar* does not mean they are the *same*, which is
// why they don't share DTOs.
public sealed record GetRecentlyCompletedSessionsParkingSession
{
    public required Guid SessionId { get; init; }
    public required Guid ParkingSpotId { get; init; }
    public required Guid UserId { get; init; }
    public required ParkingSessionStatus Status { get; init; }
    public required DateTime StartTimeUtc { get; init; }
    public required DateTime? EndTimeUtc { get; init; }
    public required string ParkingSpotName { get; init; }
}