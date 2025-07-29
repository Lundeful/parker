using Parker.Domain.Features.ParkingSessions;
using Parker.Domain.Features.ParkingSessions.Enums;

namespace Parker.Api.Features.ParkingSessions.Common.Queries;

// Example of how I often like to extract common queries or joins for reusability and readability. Depends on what I'm trying to do.
// I usually only extract when it's getting repeated *a lot*. I split up in separate files when appropriate.
public static class ParkingSessionQueries
{
    public static IQueryable<ParkingSession> WhereActive(this IQueryable<ParkingSession> query) =>
        query.Where(x => x.Status == ParkingSessionStatus.Active);

    public static IQueryable<ParkingSession> WhereCompleted(this IQueryable<ParkingSession> query) =>
        query.Where(x => x.Status == ParkingSessionStatus.Completed);

    public static IQueryable<ParkingSession> WhereCompletedWithinLastDay(this IQueryable<ParkingSession> query) =>
        query.WhereCompleted().Where(x => x.EndTimeUtc >= DateTime.UtcNow.AddDays(-1));
}