using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Parker.Api.Common.Extensions;

public static class ApiDependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("Development", policy =>
                policy.WithOrigins("http://localhost:5173", "https://localhost:5173")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });

        services.ConfigureHttpJsonOptions(o => o.SerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false)));
        services.Configure<JsonOptions>(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false)));
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SupportNonNullableReferenceTypes();
            options.UseAllOfForInheritance();
            options.UseOneOfForPolymorphism();
            options.SelectSubTypesUsing(baseType => typeof(Program).Assembly.GetTypes().Where(type => type.IsSubclassOf(baseType)));
        });

        return services;
    }

    public static WebApplication Configure(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("Development");
        }
        else
        {
            app.UseCors();
        }

        app.UseGlobalExceptionHandler();
        app.UseHttpsRedirection();
        app.MapEndpoints();

        return app;
    }
}