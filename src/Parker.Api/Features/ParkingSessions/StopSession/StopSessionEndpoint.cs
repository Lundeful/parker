using Microsoft.AspNetCore.Http.HttpResults;
using Parker.Api.Common;
using Parker.Api.Common.Extensions;
using Parker.Infrastructure.Persistence;

namespace Parker.Api.Features.ParkingSessions.StopSession;

public sealed class StopSessionEndpoint : IEndpoint
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/sessions/{sessionId:guid}/stop", StopSessionAsync)
            .WithTags(EndpointTags.Sessions);
    }

    private static async Task<Results<Ok<StopSessionResponse>, ProblemHttpResult>> StopSessionAsync(Guid sessionId,
        ParkerDbContext dbContext, CancellationToken ct)
    {
        var session = await dbContext.ParkingSessions.FindAsync([sessionId], ct);
        if (session is null)
        {
            return TypedResults.Problem(title: "Not Found", detail: "Session not found", statusCode: StatusCodes.Status404NotFound);
        }

        session.Stop();

        await dbContext.SaveChangesAsync(ct);

        var response = new StopSessionResponse
        {
            SessionId = session.Id,
            UserId = session.UserId,
            ParkingSpotId = session.ParkingSpotId,
            Status = session.Status,
            StartTimeUtc = session.StartTimeUtc,
            EndTimeUtc = session.EndTimeUtc ?? throw new InvalidOperationException("Stopped session but EndTimeUtc was null")
        };

        return TypedResults.Ok(response);
    }
}