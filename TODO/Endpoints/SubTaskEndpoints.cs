using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using TODO.Application.CQRS.Commands.SubTask.DeleteSubTask;
using TODO.Application.CQRS.Commands.SubTask.SetAsComplateSubTask;
using TODO.Application.CQRS.Commands.SubTask.SubTaskCreate;
using TODO.Application.CQRS.Commands.SubTask.SubTaskUpdate;
using TODO.Application.CQRS.Queries.SubTask.GetAllSubTask;
using TODO.Application.CQRS.Queries.SubTask.GetIncomingSubTasks;
using TODO.Application.CQRS.Queries.SubTask.GetSubTaskById;
using TODO.Core.Dto;
using TODO.Core.Dto.ProjectTask;
using TODO.Core.Enums;

namespace TODO.Endpoints
{
    public static class SubTaskEndpoints
    {
        public static void MapSubTaskEndpoints(this WebApplication app)
        {
            app.MapGet("/SubTask/GetAll", async (IMediator mediator) =>
            {
                var subtasks = await mediator.Send(new GetAllSubTasksQuery());
                return Results.Ok(subtasks);
            });

            app.MapGet("/SubTask/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var subtask = await mediator.Send(new GetSubTaskByIdQuery(id));
                return subtask is null ? Results.NotFound() : Results.Ok(subtask);
            });

            app.MapGet("/SubTask/Incoming", async (IMediator mediator, TaskPeriod period) =>
            {
                var subtasks = await mediator.Send(new GetIncomingSubTasksQuery(period));
                return Results.Ok(subtasks);
            });

            app.MapPost("/SubTask/Add", async (Guid ProjectTaskId, SubTaskCreateDTO request, IMediator mediator) =>
            {
                var created = await mediator.Send(new SubTaskCreateCommand(ProjectTaskId, request));
                return Results.Ok($"SubTask {created} was created!");
            });

            app.MapPut("/SubTask/{id:guid}", async (Guid id, SubTaskDTO request, IMediator mediator) =>
            {
                var updated = await mediator.Send(new SubTaskUpdateCommand(id, request));
                return Results.Ok($"SubTask {updated} was updated!");
            });

            app.MapPut("/SubTask/MarkAsDone/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                await mediator.Send(new SetAsComplateSubTaskCommand(id));
                return Results.Ok();
            });


            app.MapDelete("/SubTask/Delete/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                await mediator.Send(new DeleteSubTaskCommand(id));
                return Results.Ok();
            });
        }
    }
}
