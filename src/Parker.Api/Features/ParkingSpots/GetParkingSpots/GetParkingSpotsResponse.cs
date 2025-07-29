namespace Parker.Api.Features.ParkingSpots.GetParkingSpots;

// This would probably be a paginated response in a real world scenario
public sealed record GetParkingSpotsResponse
{
    public required IReadOnlyCollection<GetParkingSpotsParkingSpot> ParkingSpots { get; init; }
}

// Since different endpoints might return different versions of a "ParkingSpot" depending on where it is used, with differing details,
// I prefix it with the feature name to separate them. This could be done with namespaces, so this would stay as "ParkingSpot", but full
// names make it work well for the frontend that generate types based on OpenApi docs. Other naming schemes may work just as well, as long
// as they are unique. Just because some responses for retrieving a ParkingSpot may look *similar* does not mean they are the *same*, which is
// why they don't share DTOs.
public sealed record GetParkingSpotsParkingSpot
{
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Location { get; set; }
    public required string Type { get; set; }
    public required string Size { get; set; }
}