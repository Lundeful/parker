using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Parker.Api.Common;
using Parker.Api.Common.Extensions;
using Parker.Persistence;

namespace Parker.Api.Features.ParkingSpots.GetParkingSpots;

public sealed class GetParkingSpotsEndpoint : IEndpoint
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/spots", GetParkingSpotsAsync)
            .WithTags(EndpointTags.Spots);
    }

    private static async Task<Results<Ok<GetParkingSpotsResponse>, ProblemHttpResult>> GetParkingSpotsAsync(ParkerDbContext dbContext,
        CancellationToken ct)
    {
        var parkingSpots = await dbContext.ParkingSpots
            .Select(x => new GetParkingSpotsParkingSpot
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Location = x.Location,
                Size = x.Size,
                Type = x.Type
            })
            .ToListAsync(ct);

        var response = new GetParkingSpotsResponse
        {
            ParkingSpots = parkingSpots
        };

        return TypedResults.Ok(response);
    }
}