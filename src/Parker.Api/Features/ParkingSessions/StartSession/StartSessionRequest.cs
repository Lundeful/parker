namespace Parker.Api.Features.ParkingSessions.StartSession;

// A typical request object. Normally the user ID would come from a token or something safe, but for demo purposes it's passed in on the request.
public sealed record StartSessionRequest
{
    public required Guid UserId { get; init; }
    public required Guid ParkingSpotId { get; init; }
}

// Validators could also go here and be called via endpoint-filters, command-handler pipeline or wherever you like