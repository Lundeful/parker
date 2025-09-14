using Parker.Api.Common.Extensions;
using Parker.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi(builder.Configuration)
    .AddPersistence(builder.Configuration);

await builder
    .Build()
    .Configure()
    .RunAsync();