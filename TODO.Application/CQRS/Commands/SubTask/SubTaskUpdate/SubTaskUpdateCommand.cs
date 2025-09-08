using System;
using MediatR;
using TODO.Core.Dto;
namespace TODO.Application.CQRS.Commands.SubTask.SubTaskUpdate
{
    public class SubTaskUpdateCommand : IRequest<SubTaskDTO>
    {
        public Guid Id { get; }
        public SubTaskDTO SubTask { get; }

        public SubTaskUpdateCommand(Guid id, SubTaskDTO subTask)
        {
            Id = id;
            SubTask = subTask;
        }
    }
}
