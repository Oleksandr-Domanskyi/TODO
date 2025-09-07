using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TODO.Application.CQRS.Commands.ProjectTaskCreate;
using TODO.Application.CQRS.Queries.GetAllProjectTasks;
using TODO.Endpoints;
using TODO.Infrastructure.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetAllProjectTasksQueryHandle).Assembly);
});
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.Services.SeedDatabaseAsync();

app.MapProjectTaskEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();
