namespace Parker.Api.Features.ParkingSpots.CreateParkingSpot;

public sealed record CreateParkingSpotRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Location { get; init; }
    public required string Type { get; init; }
    public required string Size { get; init; }
}