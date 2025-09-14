using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Parker.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // Normally we could get the connection string from the configuration and fire up PostgresSQL or something, but for this demo we use an in-memory DB.
        services.AddDbContext<ParkerDbContext>(options => { options.UseInMemoryDatabase("DemoDb"); });

        return services;
    }
}