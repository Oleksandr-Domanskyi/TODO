using System;
using MediatR;

namespace TODO.Application.CQRS.Commands.SubTask.SetAsComplateSubTask
{
    public class SetAsComplateSubTaskCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public SetAsComplateSubTaskCommand(Guid id)
        {
            Id = id;
        }
    }
}
