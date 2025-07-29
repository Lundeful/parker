using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Parker.Api.Common;
using Parker.Api.Common.Extensions;
using Parker.Domain.Features.ParkingSessions;
using Parker.Infrastructure.Persistence;

namespace Parker.Api.Features.ParkingSessions.StartSession;

public sealed class StartSessionEndpoint : IEndpoint
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/sessions/start", StartSessionAsync)
            .WithTags(EndpointTags.Sessions);
    }

    private static async Task<Results<Ok<StartSessionResponse>, ProblemHttpResult>> StartSessionAsync(StartSessionRequest request,
        ParkerDbContext dbContext, CancellationToken ct)
    {
        // Auth is skipped for demo purposes. UserId passed via request instead of through tokens

        var parkingSpot = await dbContext.ParkingSpots
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.ParkingSpotId, ct);

        if (parkingSpot is null)
        {
            throw new InvalidOperationException("Cannot start session for non-existing parking spot");
        }

        var session = ParkingSession.Start(request.UserId, request.ParkingSpotId);

        dbContext.Add(session);
        await dbContext.SaveChangesAsync(ct);

        var response = new StartSessionResponse
        {
            SessionId = session.Id,
            ParkingSpotId = session.ParkingSpotId,
            UserId = session.UserId,
            Status = session.Status,
            StartTimeUtc = session.StartTimeUtc,
            ParkingSpotName = parkingSpot.Name
        };

        return TypedResults.Ok(response);
    }
}