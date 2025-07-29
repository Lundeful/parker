using System.Diagnostics;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Parker.Api.Common.Extensions;

public static class ExceptionHandler
{
    public static void UseGlobalExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = exceptionHandlerFeature?.Error;

                // Here you could add cases to handle custom exceptions or other known failures
                var problemDetails = exception switch
                {
                    UnauthorizedAccessException unauthorized => new ProblemDetails
                    {
                        Status = StatusCodes.Status401Unauthorized,
                        Title = "Unauthorized",
                        Detail = unauthorized.Message,
                        Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/401",
                        Extensions = { ["code"] = "Error.Unauthorized" }
                    },

                    // Example using Fluent Validation exceptions
                    ValidationException validationException => new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Validation Error",
                        Detail = validationException.Message,
                        Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/422",
                        Extensions =
                        {
                            ["code"] = "Error.Validation",
                            ["errors"] = validationException.Errors?.Select(e => new { e.PropertyName, e.ErrorMessage }).ToList()
                        }
                    },

                    // Fallback, to not expose any data through unexpected exceptions
                    _ => new ProblemDetails
                    {
                        Status = StatusCodes.Status500InternalServerError,
                        Title = "Internal Server Error",
                        Detail = "An unexpected error occurred",
                        Type = "https://developer.mozilla.org/en-US/docs/Web/HTTP/Status/500",
                        Extensions =
                        {
                            ["code"] = "InternalServerError",
                            ["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier
                        }
                    }
                };

                problemDetails.Instance = context.Request.Path;
                context.Response.StatusCode = problemDetails.Status!.Value;
                context.Response.ContentType = "application/problem+json";

                var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

                if (problemDetails.Status == 500)
                {
                    logger.LogError(exception, "An unhandled exception occurred. TraceId: {TraceId}", problemDetails.Extensions["traceId"]);
                }

                await context.Response.WriteAsJsonAsync(problemDetails);
            });
        });
    }
}