using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parker.Infrastructure.Persistence;

namespace Parker.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Normally we could get the connection string from the configuration and fire up PostgresSQL or something, but for this demo we use an in-memory DB.
        services.AddDbContext<ParkerDbContext>(options => { options.UseInMemoryDatabase("DemoDb"); });

        return services;
    }
}