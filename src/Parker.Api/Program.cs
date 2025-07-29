using Parker.Api.Common.Extensions;
using Parker.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

await builder
    .Build()
    .Configure()
    .RunAsync();