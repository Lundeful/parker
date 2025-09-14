using Microsoft.AspNetCore.Http.HttpResults;
using Parker.Api.Common;
using Parker.Api.Common.Extensions;
using Parker.Domain.Features.ParkingSpots;
using Parker.Persistence;

namespace Parker.Api.Features.ParkingSpots.CreateParkingSpot;

public sealed class CreateParkingSpotEndpoint : IEndpoint
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/admin/spots", CreateParkingSpotAsync)
            .WithTags(EndpointTags.Spots);
    }

    private static async Task<Results<Ok<CreateParkingSpotResponse>, ProblemHttpResult>> CreateParkingSpotAsync(
        CreateParkingSpotRequest request,
        ParkerDbContext dbContext,
        CancellationToken ct)
    {
        var parkingSpot = ParkingSpot.Create(
            request.Name,
            request.Description,
            request.Location,
            request.Type,
            request.Size);

        dbContext.Add(parkingSpot);
        await dbContext.SaveChangesAsync(ct);

        var response = new CreateParkingSpotResponse
        {
            Id = parkingSpot.Id,
            Name = parkingSpot.Name,
            Description = parkingSpot.Description,
            Location = parkingSpot.Location,
            Type = parkingSpot.Type,
            Size = parkingSpot.Size
        };

        return TypedResults.Ok(response);
    }
}