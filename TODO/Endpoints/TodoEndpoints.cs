// ProjectTaskEndpoints.cs
using MediatR;
using TODO.Application.CQRS.Queries.GetAllProjectTasks;
using TODO.Application.CQRS.Queries.GetProjectTaskById;
using TODO.Application.CQRS.Queries.GetIncomingProjectTask;
using TODO.Application.CQRS.Commands.ProjectTaskCreate;
using TODO.Application.CQRS.Commands.ProjectTaskUpdate;
using TODO.Application.CQRS.Commands.SetProjectTaskPercent;
using TODO.Core.Dto.ProjectTask;
using TODO.Application.CQRS.Commands.DeleteProjectTask;
using TODO.Core.Enums;

namespace TODO.Endpoints;

public static class ProjectTaskEndpoints
{
    public static void MapProjectTaskEndpoints(this WebApplication app)
    {
        app.MapGet("/ProjectTask/GetAll", async (IMediator mediator) =>
        {
            var todos = await mediator.Send(new GetAllProjectTasksQuery());
            return Results.Ok(todos);
        });

        app.MapGet("/ProjectTask/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            var todo = await mediator.Send(new GetProjectTaskByIdQuery(id));
            return todo is null ? Results.NotFound() : Results.Ok(todo);
        });

        app.MapGet("/ProjectTask/Incoming", async (IMediator mediator, TaskPeriod period) =>
        {
            var todos = await mediator.Send(new GetIncomingProjectTaskQuery(period));
            return Results.Ok(todos);
        });

        app.MapPost("/ProjectTask/Add", async (ProjectTaskCreateDTO request, IMediator mediator) =>
        {
            var created = await mediator.Send(new ProjectTaskCreateCommand(request));
            return Results.Ok($"ProjectTask {created} was created!!!");
        });

        app.MapPut("/ProjectTask/{id:guid}", async (Guid id, ProjectTaskUpdateDTO request, IMediator mediator) =>
        {
            var updated = await mediator.Send(new ProjectTaskUpdateCommand(id, request));
            return Results.Ok($"ProjectTask {updated} was updated!!!");
        });

        app.MapPut("/ProjectTask/{id:guid}/SetAsComplate", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new SetAsComplateProjectTaskPercentCommand(id));
            return Results.Ok();
        });

        app.MapDelete("/ProjectTask/Delete/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteProjectTaskCommand(id));
            return Results.Ok();
        });
    }
}
