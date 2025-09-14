using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Parker.Api.Common;
using Parker.Api.Common.Extensions;
using Parker.Api.Features.ParkingSessions.Common.Queries;
using Parker.Persistence;

namespace Parker.Api.Features.ParkingSessions.GetRecentlyCompletedSessions;

public sealed class GetRecentlyCompletedSessionsEndpoint : IEndpoint
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/sessions/recently-completed", GetRecentlyCompletedSessionsAsync)
            .WithTags(EndpointTags.Sessions);
    }

    private static async Task<Results<Ok<GetRecentlyCompletedSessionsResponse>, ProblemHttpResult>> GetRecentlyCompletedSessionsAsync(
        ParkerDbContext dbContext, CancellationToken ct)
    {
        var sessions = await dbContext.ParkingSessions
            .WhereCompletedWithinLastDay()
            .Join(dbContext.ParkingSpots, session => session.ParkingSpotId, spot => spot.Id,
                (session, spot) =>
                    new
                    {
                        Session = session,
                        Spot = spot
                    })
            .Select(x => new GetRecentlyCompletedSessionsParkingSession
            {
                SessionId = x.Session.Id,
                ParkingSpotId = x.Session.ParkingSpotId,
                UserId = x.Session.UserId,
                Status = x.Session.Status,
                StartTimeUtc = x.Session.StartTimeUtc,
                EndTimeUtc = x.Session.EndTimeUtc,
                ParkingSpotName = x.Spot.Name
            })
            .ToListAsync(ct);

        var response = new GetRecentlyCompletedSessionsResponse
        {
            Sessions = sessions
        };

        return TypedResults.Ok(response);
    }
}