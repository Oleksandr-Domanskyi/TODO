using System;
using MediatR;
using TODO.Core.Dto;
using TODO.Core.Dto.ProjectTask;

namespace TODO.Application.CQRS.Commands.SubTask.SubTaskCreate
{
    public class SubTaskCreateCommand : IRequest<SubTaskDTO>
    {
        public Guid ProjectTaskId { get; }
        public SubTaskCreateDTO SubTask { get; }

        public SubTaskCreateCommand(Guid projectTaskId, SubTaskCreateDTO subTask)
        {
            ProjectTaskId = projectTaskId;
            SubTask = subTask;
        }
    }
}
