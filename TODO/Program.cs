using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TODO.Application.CQRS.Commands.ProjectTaskCreate;
using TODO.Application.CQRS.Queries.GetAllProjectTasks;
using TODO.Endpoints;
using TODO.Infrastructure.Extension;
using TODO.Application.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Seeder
await app.Services.SeedDatabaseAsync();

app.MapProjectTaskEndpoints();
app.MapSubTaskEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
