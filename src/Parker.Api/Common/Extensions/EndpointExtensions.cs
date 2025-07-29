using System.Reflection;

namespace Parker.Api.Common.Extensions;

public interface IEndpoint
{
    void DefineEndpoints(IEndpointRouteBuilder app);
}

public static class EndpointExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        // Need to clean up OpenAPI docs by combining common ProducesProblem stuff. It gets pretty wordy right now, but it works when generating types
        var group = app.MapGroup("/")
            .WithOpenApi()
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status404NotFound);

        var endpointTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => typeof(IEndpoint).IsAssignableFrom(type) && type is { IsInterface: false, IsAbstract: false });

        foreach (var type in endpointTypes)
        {
            var endpointInstance = Activator.CreateInstance(type) as IEndpoint;
            endpointInstance?.DefineEndpoints(group);
        }
    }
}